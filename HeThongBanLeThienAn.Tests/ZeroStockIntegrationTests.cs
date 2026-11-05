using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Controllers;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HeThongBanLeThienAn.Tests
{
    /// <summary>
    /// Kiểm thử tích hợp (Integration Tests) cho dữ liệu sản phẩm 21–100 (StockQuantity = 0).
    /// Mục tiêu: Đảm bảo POS từ chối thanh toán đúng đắn với các sản phẩm hết hàng,
    /// và filter-stock nhận diện đúng sản phẩm cần nhập kho.
    /// </summary>
    public class ZeroStockIntegrationTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly SupermarketDbContext _db;
        private readonly CheckoutService _checkout;
        private readonly IDbConnectionFactory _factory;

        public ZeroStockIntegrationTests()
        {
            string cs = $"Data Source=zerostock_{Guid.NewGuid():N};Mode=Memory;Cache=Shared;Foreign Keys=True";
            _connection = new SqliteConnection(cs);
            _connection.Open();

            _db = new SupermarketDbContext(
                new DbContextOptionsBuilder<SupermarketDbContext>()
                    .UseSqlite(_connection)
                    .Options);
            _db.Database.EnsureCreated();
            CustomerFixtures.Add(_db);

            _factory = new SqliteTestDbConnectionFactory(cs);
            _checkout = new CheckoutService(_factory);
        }

        // ─────────────────────────────────────────────────────────────────
        // A. Dữ liệu gốc
        // ─────────────────────────────────────────────────────────────────

        [Fact]
        public void SeedData_Products21To100_HaveZeroStock()
        {
            // Seed tạo đúng 100 sản phẩm, ID 21–100 đều có StockQuantity = 0
            var zeroStockProducts = _db.Products
                .Where(p => p.ProductId > 20)
                .ToList();

            Assert.Equal(80, zeroStockProducts.Count);
            Assert.All(zeroStockProducts, p =>
                Assert.Equal(0, p.StockQuantity));
        }

        [Fact]
        public void SeedData_Products1To20_HavePositiveStock()
        {
            // Chỉ sản phẩm 1–20 có tồn kho dương — dùng được trong demo POS
            var inStockProducts = _db.Products
                .Where(p => p.ProductId <= 20)
                .ToList();

            Assert.Equal(20, inStockProducts.Count);
            Assert.All(inStockProducts, p =>
                Assert.True(p.StockQuantity > 0,
                    $"Sản phẩm ID={p.ProductId} phải có StockQuantity > 0"));
        }

        [Fact]
        public void SeedData_AllBarcodes_AreNumericAndDistinct()
        {
            var barcodes = _db.Products.Select(p => p.Barcode).ToList();
            Assert.Equal(100, barcodes.Distinct().Count());
            Assert.All(barcodes, b => Assert.Matches("^[0-9]+$", b));
        }

        // ─────────────────────────────────────────────────────────────────
        // B. Thanh toán POS: sản phẩm hết hàng bị từ chối
        // ─────────────────────────────────────────────────────────────────

        [Theory]
        [InlineData(21)]   // Sản phẩm đầu tiên hết hàng
        [InlineData(50)]   // Giữa vùng hết hàng
        [InlineData(100)]  // Sản phẩm cuối cùng
        public async Task Checkout_ZeroStockProduct_ThrowsInvalidOperation(int productId)
        {
            // Xác nhận sản phẩm thực sự có StockQuantity = 0
            var product = _db.Products.Single(p => p.ProductId == productId);
            Assert.Equal(0, product.StockQuantity);

            var cart = new List<CartItemCheckoutDto>
            {
                new CartItemCheckoutDto { ProductId = productId, Quantity = 1, UnitPrice = product.Price }
            };

            // POS phải từ chối ngay — không được commit gì vào DB
            int ordersBefore = _db.Orders.Count();
            await Assert.ThrowsAsync<InvalidOperationException>(() => _checkout.Checkout("cashier01", null, null, cart));

            // Rollback hoàn toàn: số đơn không tăng
            Assert.Equal(ordersBefore, _db.Orders.Count());
        }

        [Fact]
        public async Task Checkout_ZeroStockProduct_DoesNotChangeAnyStock()
        {
            // Mua sản phẩm 21 (hết hàng) → tất cả StockQuantity giữ nguyên
            var cart = new List<CartItemCheckoutDto>
            {
                new CartItemCheckoutDto { ProductId = 21, Quantity = 1, UnitPrice = 10000m }
            };

            var stocksBefore = _db.Products.AsNoTracking()
                .ToDictionary(p => p.ProductId, p => p.StockQuantity);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _checkout.Checkout("cashier01", null, null, cart));

            var stocksAfter = _db.Products.AsNoTracking()
                .ToDictionary(p => p.ProductId, p => p.StockQuantity);

            foreach (var kvp in stocksBefore)
                Assert.Equal(kvp.Value, stocksAfter[kvp.Key]);
        }

        [Fact]
        public async Task Checkout_MixedCart_InStockAndZeroStock_RejectsEntireCart()
        {
            // Giỏ hàng có cả sản phẩm còn hàng (ID=1) lẫn hết hàng (ID=21)
            // → Toàn bộ giao dịch phải bị Rollback
            int stockBefore = _db.Products.AsNoTracking().Single(p => p.ProductId == 1).StockQuantity;
            int ordersBefore = _db.Orders.Count();

            var cart = new List<CartItemCheckoutDto>
            {
                new CartItemCheckoutDto { ProductId = 1,  Quantity = 1, UnitPrice = 12500m },
                new CartItemCheckoutDto { ProductId = 21, Quantity = 1, UnitPrice = 10000m }
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => _checkout.Checkout("cashier01", null, null, cart));

            // Sản phẩm 1 phải không bị giảm tồn kho
            int stockAfter = _db.Products.AsNoTracking().Single(p => p.ProductId == 1).StockQuantity;
            Assert.Equal(stockBefore, stockAfter);
            Assert.Equal(ordersBefore, _db.Orders.Count());
        }

        // ─────────────────────────────────────────────────────────────────
        // C. ADO.NET FilterStock nhận diện đúng sản phẩm cần nhập kho
        // ─────────────────────────────────────────────────────────────────

        [Fact]
        public async Task AdoFilterStock_WithThreshold0_ReturnsAll80ZeroStockProducts()
        {
            var controller = new AdoProductController(null!, _factory);
            var result = await controller.FilterStock(0) as OkObjectResult;

            Assert.NotNull(result);
            var report = Assert.IsType<StockFilterReportDto>(result!.Value);
            Assert.Equal(0, report.MinStockThreshold);
            Assert.Equal(80, report.TotalFound);
            Assert.All(report.Data, p => Assert.Equal(0, p.StockQuantity));
        }

        [Fact]
        public async Task AdoFilterStock_WithThreshold500_ReturnsAllProducts()
        {
            var controller = new AdoProductController(null!, _factory);
            var result = await controller.FilterStock(500) as OkObjectResult;

            Assert.NotNull(result);
            var report = Assert.IsType<StockFilterReportDto>(result!.Value);
            // Tất cả 100 sản phẩm có StockQuantity <= 500 (sản phẩm tồn kho cao nhất là 500)
            Assert.Equal(100, report.TotalFound);
        }

        [Fact]
        public async Task AdoBarcode_ZeroStockProduct_StillReturnProductInfo()
        {
            // API vẫn trả thông tin sản phẩm dù StockQuantity = 0
            // (kiểm tra tồn kho xảy ra ở CheckoutService, không phải barcode lookup)
            var product = _db.Products.First(p => p.ProductId == 21);
            var controller = new AdoProductController(null!, _factory);
            var result = await controller.GetProductByBarcode(product.Barcode) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result!.StatusCode);
            var dto = Assert.IsType<ProductLookupDto>(result.Value);
            Assert.Equal(0, dto.StockQuantity);
            Assert.Equal(product.Barcode, dto.Barcode);
        }

        // ─────────────────────────────────────────────────────────────────
        // D. EF Core API: Phân trang và tìm kiếm sản phẩm
        // ─────────────────────────────────────────────────────────────────

        [Fact]
        public async System.Threading.Tasks.Task ProductsController_GetPaged_Page2_ReturnsZeroStockProducts()
        {
            // Page 1 = sản phẩm 1-10, Page 2 = sản phẩm 11-20, Page 3 = sản phẩm 21-30 (hết hàng)
            var controller = new ProductsController(_db);
            var action = await controller.GetPagedProducts(null, null, 3, 10);
            var result = Assert.IsType<OkObjectResult>(action.Result);
            var page = Assert.IsType<PagedResult<ProductDto>>(result.Value);

            Assert.Equal(3, page.CurrentPage);
            Assert.Equal(10, page.Items.Count);
            // Sản phẩm trang 3 (ID 21–30) đều hết hàng
            Assert.All(page.Items, p => Assert.Equal(0, p.StockQuantity));
        }

        [Fact]
        public async System.Threading.Tasks.Task ProductsController_GetAll_Returns100Products()
        {
            var controller = new ProductsController(_db);
            var action = await controller.GetAll();
            var result = Assert.IsType<OkObjectResult>(action);
            var products = Assert.IsType<System.Collections.Generic.List<ProductDto>>(result.Value);
            Assert.Equal(100, products.Count);
        }

        public void Dispose()
        {
            _db.Dispose();
            _connection.Dispose();
        }
    }
}

