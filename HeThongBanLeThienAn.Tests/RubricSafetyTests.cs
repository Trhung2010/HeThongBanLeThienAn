using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Controllers;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using Xunit;

namespace HeThongBanLeThienAn.Tests;

public sealed class RubricSafetyTests : IDisposable
{
    private readonly SqliteConnection connection = new("Data Source=:memory:");
    private readonly SupermarketDbContext db;
    public RubricSafetyTests()
    {
        connection.Open();
        db = new(new DbContextOptionsBuilder<SupermarketDbContext>().UseSqlite(connection).Options);
        db.Database.EnsureCreated();
    }
    public void Dispose() { db.Dispose(); connection.Dispose(); }

    [Fact]
    public async Task InvoiceHistorySurvivesControllerAndDirectDatabaseDeletion()
    {
        int count = await db.OrderDetails.CountAsync();
        var detail = await db.OrderDetails.AsNoTracking().FirstAsync();
        var product = await db.Products.AsNoTracking().SingleAsync(p => p.ProductId == detail.ProductId);
        Assert.IsType<ConflictObjectResult>(await new ProductsController(db).Delete(product.ProductId));
        Assert.IsType<ConflictObjectResult>(await new CategoriesController(db).Delete(product.CategoryId));
        await Assert.ThrowsAsync<SqliteException>(() => db.Database.ExecuteSqlInterpolatedAsync(
            $"DELETE FROM Products WHERE ProductId={product.ProductId}"));
        await Assert.ThrowsAsync<SqliteException>(() => db.Database.ExecuteSqlInterpolatedAsync(
            $"DELETE FROM Categories WHERE CategoryId={product.CategoryId}"));
        Assert.Equal(count, await db.OrderDetails.CountAsync());
    }

    [Fact]
    public async Task UnusedProductAndEmptyCategoryCanStillBeDeleted()
    {
        var category = new Category { CategoryName = "Empty test" };
        db.Categories.Add(category);
        await db.SaveChangesAsync();
        var product = new Product { CategoryId = category.CategoryId, Barcode = "999999", ProductName = "Unused", Price = 1 };
        db.Products.Add(product);
        await db.SaveChangesAsync();
        Assert.IsType<NoContentResult>(await new ProductsController(db).Delete(product.ProductId));
        Assert.IsType<NoContentResult>(await new CategoriesController(db).Delete(category.CategoryId));
    }

    [Fact]
    public async Task CashierCannotMintPointsWhenCreatingOrUpdatingCustomer()
    {
        var controller = new CustomersController(db)
        {
            ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, "Cashier") }, "Test"))
            } }
        };
        var request = new CustomerUpsertDto { CustomerName = "Test", PhoneNumber = "0901234567", RewardPoints = 100000, MembershipRank = "Vàng" };
        var created = Assert.IsType<CreatedAtActionResult>(await controller.Create(request));
        var dto = Assert.IsType<CustomerDto>(created.Value);
        Assert.Equal(0, dto.RewardPoints);
        Assert.Equal("Chuẩn", dto.MembershipRank);
        var customer = await db.Customers.FindAsync(dto.CustomerId);
        customer!.RewardPoints = 75;
        await db.SaveChangesAsync();
        request.CustomerName = "Updated";
        Assert.IsType<NoContentResult>(await controller.Update(dto.CustomerId, request));
        db.ChangeTracker.Clear();
        customer = await db.Customers.FindAsync(dto.CustomerId);
        Assert.Equal(75, customer!.RewardPoints);
        Assert.Equal("Chuẩn", customer.MembershipRank);
        Assert.Equal("Updated", customer.CustomerName);
    }

    [Fact]
    public async Task DuplicateKeysAndMissingCategoryProduceBusinessErrors()
    {
        var product = await db.Products.AsNoTracking().FirstAsync();
        var request = new ProductUpsertDto { ProductName = "Test", Barcode = product.Barcode, CategoryId = product.CategoryId, Price = 10 };
        var products = new ProductsController(db);
        Assert.IsType<ConflictObjectResult>(await products.Create(request));
        request.Barcode = "999991";
        request.CategoryId = int.MaxValue;
        Assert.IsType<BadRequestObjectResult>(await products.Create(request));
        Assert.IsType<BadRequestObjectResult>(await products.Update(product.ProductId, request));
        var category = await db.Categories.FirstAsync();
        Assert.IsType<ConflictObjectResult>(await new CategoriesController(db).Create(new() { CategoryName = category.CategoryName }));
        db.Customers.Add(new Customer { CustomerName = "Test", PhoneNumber = "0901111111" });
        await db.SaveChangesAsync();
        Assert.IsType<ConflictObjectResult>(await new CustomersController(db).Create(new() { CustomerName = "Duplicate", PhoneNumber = "0901111111" }));
    }

    [Fact]
    public async Task TodayKpiIsIndependentOfSelectedHistoricalPeriod()
    {
        var today = DateTime.Today;
        db.Orders.AddRange(
            new Order { OrderCode = "TEST-TODAY", CashierUsername = "admin", CreatedDate = today, FinalAmount = 12345m },
            new Order { OrderCode = "TEST-OLD", CashierUsername = "admin", CreatedDate = today.AddDays(-1), FinalAmount = 999m });
        await db.SaveChangesAsync();
        var expected = (await db.Orders.Where(o => o.CreatedDate >= today && o.CreatedDate < today.AddDays(1)).Select(o => o.FinalAmount).ToListAsync()).Sum();
        var result = Assert.IsType<OkObjectResult>(await new ReportsController(db).GetDashboardSummary(today.AddDays(-1), today.AddDays(-1)));
        var report = Assert.IsType<DashboardSummaryDto>(result.Value);
        Assert.Equal(expected, report.RevenueToday);
        Assert.Single(report.RevenueSeries);
        Assert.Equal(today.AddDays(-1), report.RevenueSeries[0].Date);
    }
}
