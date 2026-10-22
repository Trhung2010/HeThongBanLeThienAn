using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Controllers;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HeThongBanLeThienAn.Tests
{
    public class Buoi5AdoNetTests
    {
        private (SupermarketDbContext context, IDbConnectionFactory factory, SqliteConnection conn) SetupTestDb()
        {
            var connection = new SqliteConnection("DataSource=file:memdb_buoi5_" + Guid.NewGuid() + "?mode=memory&cache=shared");
            connection.Open();

            var options = new DbContextOptionsBuilder<SupermarketDbContext>()
                .UseSqlite(connection)
                .Options;

            var context = new SupermarketDbContext(options);
            context.Database.EnsureCreated();
            CustomerFixtures.Add(context);

            var factory = new SqliteTestDbConnectionFactory(connection.ConnectionString);
            return (context, factory, connection);
        }

        // =====================================================================
        // TEST TIẾT 2: TRA CỨU BARCODE & DISCONNECTED CACHE & BÀI TẬP 2.1, 2.2
        // =====================================================================
        [Fact]
        public void AdoProductController_GetProductByBarcode_ReturnsSuccess()
        {
            var (_, factory, conn) = SetupTestDb();
            try
            {
                var controller = new AdoProductController(null!, factory);
                var result = controller.GetProductByBarcode("893456789001") as OkObjectResult;

                Assert.NotNull(result);
                Assert.Equal(200, result.StatusCode);
                var dto = result.Value as ProductLookupDto;
                Assert.NotNull(dto);
                Assert.Equal("893456789001", dto.Barcode);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void AdoProductController_GetProductByBarcode_NotFound_Returns404()
        {
            var (_, factory, conn) = SetupTestDb();
            try
            {
                var controller = new AdoProductController(null!, factory);
                var result = controller.GetProductByBarcode("000000000000") as NotFoundObjectResult;

                Assert.NotNull(result);
                Assert.Equal(404, result.StatusCode);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void AdoProductController_GetProductsDisconnected_Returns20CachedProducts()
        {
            var (_, factory, conn) = SetupTestDb();
            try
            {
                var controller = new AdoProductController(null!, factory);
                var result = controller.GetProductsDisconnected() as OkObjectResult;

                Assert.NotNull(result);
                Assert.Equal(200, result.StatusCode);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void AdoProductController_FilterStock_ReturnsFilteredProducts()
        {
            var (_, factory, conn) = SetupTestDb();
            try
            {
                var controller = new AdoProductController(null!, factory);
                var result = controller.FilterStock(50) as OkObjectResult;

                Assert.NotNull(result);
                Assert.Equal(200, result.StatusCode);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void AdoProductController_CountByCategory_ReturnsTotalCategories()
        {
            var (_, factory, conn) = SetupTestDb();
            try
            {
                var controller = new AdoProductController(null!, factory);
                var result = controller.CountByCategory() as OkObjectResult;

                Assert.NotNull(result);
                Assert.Equal(200, result.StatusCode);
            }
            finally
            {
                conn.Close();
            }
        }

        // =====================================================================
        // TEST TIẾT 3: BÁO CÁO DOANH THU & TOP SELLING PRODUCTS
        // =====================================================================
        [Fact]
        public void ReportsController_GetRevenueByCashier_ReturnsAllActiveCashiers()
        {
            var (_, factory, conn) = SetupTestDb();
            try
            {
                var controller = new ReportsController(null!, factory);
                var result = controller.GetRevenueByCashier() as OkObjectResult;

                Assert.NotNull(result);
                Assert.Equal(200, result.StatusCode);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void ReportsController_GetRevenueByDateRange_ReturnsCorrectData()
        {
            var (_, factory, conn) = SetupTestDb();
            try
            {
                var controller = new ReportsController(null!, factory);
                var result = controller.GetRevenueByDateRange(new DateTime(2026, 10, 1), new DateTime(2026, 10, 31)) as OkObjectResult;

                Assert.NotNull(result);
                Assert.Equal(200, result.StatusCode);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void ReportsController_GetTop5SellingProducts_ReturnsTopItems()
        {
            var (_, factory, conn) = SetupTestDb();
            try
            {
                var controller = new ReportsController(null!, factory);
                var result = controller.GetTop5SellingProducts() as OkObjectResult;

                Assert.NotNull(result);
                Assert.Equal(200, result.StatusCode);
            }
            finally
            {
                conn.Close();
            }
        }

        // =====================================================================
        // TEST TIẾT 5: CHECKOUT BỌC TRANSACTION (ACID) & ROLLBACK KHI THẤT BẠI
        // =====================================================================
        [Fact]
        public void CheckoutController_ProcessCheckout_Success_CommitsAndReducesStock()
        {
            var (context, factory, conn) = SetupTestDb();
            try
            {
                var controller = new CheckoutController(null!, factory);
                var initialProduct = context.Products.First(p => p.ProductId == 1);
                int initialStock = initialProduct.StockQuantity;

                var request = new PosCheckoutRequestDto
                {
                    CashierUsername = "cashier01",
                    CustomerId = 1,
                    Items = new List<CartItemCheckoutDto>
                    {
                        new CartItemCheckoutDto { ProductId = 1, Quantity = 5, UnitPrice = 12500m }
                    }
                };

                // Act
                var result = controller.ProcessCheckout(request) as OkObjectResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal(200, result.StatusCode);

                // Verify DB changed
                context.Entry(initialProduct).Reload();
                Assert.Equal(initialStock - 5, initialProduct.StockQuantity);
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void CheckoutController_ProcessCheckout_ExceedStock_RollsBackCleanly()
        {
            var (context, factory, conn) = SetupTestDb();
            try
            {
                var controller = new CheckoutController(null!, factory);
                var initialProduct = context.Products.First(p => p.ProductId == 1);
                int initialStock = initialProduct.StockQuantity;
                int initialOrderCount = context.Orders.Count();

                var request = new PosCheckoutRequestDto
                {
                    CashierUsername = "cashier01",
                    CustomerId = 1,
                    Items = new List<CartItemCheckoutDto>
                    {
                        // Yêu cầu mua 99999 món (vượt quá tồn kho thực tế)
                        new CartItemCheckoutDto { ProductId = 1, Quantity = 99999, UnitPrice = 12500m }
                    }
                };

                // Act
                var result = controller.ProcessCheckout(request) as BadRequestObjectResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal(400, result.StatusCode);

                // Verify Rollback: Tồn kho và số hóa đơn nguyên vẹn
                context.Entry(initialProduct).Reload();
                Assert.Equal(initialStock, initialProduct.StockQuantity);
                Assert.Equal(initialOrderCount, context.Orders.Count());
            }
            finally
            {
                conn.Close();
            }
        }

        [Fact]
        public void CheckoutController_CancelOrder_RestoresStockAndPoints()
        {
            var (context, factory, conn) = SetupTestDb();
            try
            {
                var controller = new CheckoutController(null!, factory);

                // Bước 1: Tạo hóa đơn mua 2 SP
                var request = new PosCheckoutRequestDto
                {
                    CashierUsername = "cashier02",
                    CustomerId = 2,
                    Items = new List<CartItemCheckoutDto>
                    {
                        new CartItemCheckoutDto { ProductId = 2, Quantity = 2, UnitPrice = 135000m }
                    }
                };
                var checkoutRes = controller.ProcessCheckout(request) as OkObjectResult;
                Assert.NotNull(checkoutRes);

                // Lấy ID hóa đơn vừa tạo
                dynamic val = checkoutRes.Value!;
                int orderId = (int)val.GetType().GetProperty("orderId").GetValue(val, null);

                // Bước 2: Hủy hóa đơn vừa tạo
                var cancelRes = controller.CancelOrder(orderId) as OkObjectResult;
                Assert.NotNull(cancelRes);
                Assert.Equal(200, cancelRes.StatusCode);

                // Kiểm tra hóa đơn đã bị xóa
                var deletedOrder = context.Orders.FirstOrDefault(o => o.OrderId == orderId);
                Assert.Null(deletedOrder);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
