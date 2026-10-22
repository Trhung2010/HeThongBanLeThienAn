using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using MiniSupermarket.API.Controllers;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using Xunit;

namespace HeThongBanLeThienAn.Tests;

public sealed class CheckoutRegressionTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly SupermarketDbContext _db;
    private readonly CheckoutService _checkout;
    private readonly IConfiguration _config;
    public CheckoutRegressionTests()
    {
        string cs = $"Data Source=regression_{Guid.NewGuid():N};Mode=Memory;Cache=Shared;Foreign Keys=True";
        _connection = new SqliteConnection(cs);
        _connection.Open();
        _db = new SupermarketDbContext(new DbContextOptionsBuilder<SupermarketDbContext>().UseSqlite(_connection).Options);
        _db.Database.Migrate();
        CustomerFixtures.Add(_db);
        _config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string,string?> {
            ["ConnectionStrings:DefaultConnection"] = cs,
            ["JwtSettings:Secret"] = "SupermarketSecretKeyDoAnMonHoc2026SecureString!!"
        }).Build();
        _checkout = new CheckoutService(new DbConnectionFactory(_config));
    }
    private static List<CartItemCheckoutDto> Cart(int quantity = 1, decimal price = 1) =>
        new() { new() { ProductId=1, Quantity=quantity, UnitPrice=price } };
    private int Stock => _db.Products.AsNoTracking().Single(p=>p.ProductId==1).StockQuantity;
    private int Points => _db.Customers.AsNoTracking().Single(c=>c.CustomerId==1).RewardPoints;
    private JsonElement Json(IActionResult result) => JsonSerializer.SerializeToElement(Assert.IsType<OkObjectResult>(result).Value);

    [Fact]
    public void Migrations_Create100DistinctReferenceItemsAndConsistentInvoices()
    {
        Assert.Equal(100, _db.Products.Count());
        Assert.Equal(100, _db.Products.Select(p=>p.Barcode).Distinct().Count());
        Assert.All(_db.Products.ToList(), p=>Assert.Contains("Tham khảo", p.DataSource));
        Assert.All(_db.Products.Where(p=>p.ProductId>20).ToList(), p=>Assert.Equal(0,p.StockQuantity));
        foreach (var order in _db.Orders.Include(o=>o.OrderDetails))
        {
            Assert.NotEmpty(order.OrderDetails);
            Assert.Equal(order.TotalItems,order.OrderDetails.Sum(d=>d.Quantity));
            Assert.Equal(order.FinalAmount,order.OrderDetails.Sum(d=>d.LineTotal));
            Assert.All(order.OrderDetails,d=>Assert.Equal(d.Quantity*d.UnitPrice,d.LineTotal));
        }
        Assert.True(_db.Database.GetAppliedMigrations().Count() >= 3);
    }

    [Fact]
    public void Migration_PreservesBusinessEditsAndAdditionalRowsAndIsRepeatable()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        using var db = new SupermarketDbContext(new DbContextOptionsBuilder<SupermarketDbContext>().UseSqlite(connection).Options);
        db.GetService<IMigrator>().Migrate("20261015104746_InitialCreateFullThienAn");
        db.Database.ExecuteSqlRaw("UPDATE Products SET StockQuantity=123, Price=777 WHERE ProductId=1");
        db.Database.ExecuteSqlRaw("UPDATE Customers SET CustomerName='Edited customer',PhoneNumber='CUSTOM-1' WHERE CustomerId=1");
        db.Database.ExecuteSqlRaw("INSERT INTO Products (ProductId,Barcode,ProductName,Price,StockQuantity,CategoryId) VALUES(101,'BUSINESS-101','User product',500,7,1)");
        db.Database.Migrate();
        db.Database.Migrate();
        Assert.Equal(101,db.Products.Count());
        Assert.Equal(123,db.Products.Single(p=>p.ProductId==1).StockQuantity);
        Assert.Equal(777m,db.Products.Single(p=>p.ProductId==1).Price);
        Assert.Equal(7,db.Products.Single(p=>p.ProductId==101).StockQuantity);
        Assert.Equal("Edited customer",db.Customers.Single(c=>c.CustomerId==1).CustomerName);
        Assert.Equal(1,db.Orders.Single(o=>o.OrderId==1).CustomerId);
    }

    [Fact]
    public void Checkout_UsesDatabasePriceAndCommitsAllTables()
    {
        int stock=Stock, points=Points, orders=_db.Orders.Count();
        var receipt=_checkout.Checkout("cashier01",1,null,Cart(2,0.01m));
        Assert.Equal(25000m,receipt.FinalAmount);
        Assert.Equal(stock-2,Stock);
        Assert.Equal(points+2,Points);
        Assert.Equal(orders+1,_db.Orders.Count());
        var detail=_db.OrderDetails.Single(d=>d.OrderId==receipt.OrderId);
        Assert.Equal(12500m,detail.UnitPrice);
        Assert.Equal(receipt.FinalAmount,detail.LineTotal);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(99999)]
    public void Checkout_RejectsInvalidQuantitiesWithoutMutation(int quantity)
    {
        int stock=Stock, orders=_db.Orders.Count(), points=Points;
        Assert.Throws<InvalidOperationException>(()=>_checkout.Checkout("cashier01",1,null,Cart(quantity)));
        Assert.Equal(stock,Stock); Assert.Equal(orders,_db.Orders.Count()); Assert.Equal(points,Points);
    }

    [Fact]
    public void Checkout_DuplicateLinesCannotExceedStock()
    {
        int stock=Stock, orders=_db.Orders.Count();
        var cart=Cart(stock); cart.Add(new() { ProductId=1, Quantity=1 });
        Assert.Throws<InvalidOperationException>(()=>_checkout.Checkout("cashier01",1,null,cart));
        Assert.Equal(stock,Stock); Assert.Equal(orders,_db.Orders.Count());
    }

    [Fact]
    public void Checkout_LateDatabaseFailureRollsBackOrderStockDetailsAndPoints()
    {
        int stock=Stock, points=Points, orders=_db.Orders.Count(), details=_db.OrderDetails.Count();
        _db.Database.ExecuteSqlRaw("CREATE TRIGGER RejectNewDetail BEFORE INSERT ON OrderDetails BEGIN SELECT RAISE(ABORT,'test failure'); END;");
        Assert.Throws<SqliteException>(()=>_checkout.Checkout("cashier01",1,null,Cart(2)));
        Assert.Equal(stock,Stock); Assert.Equal(points,Points);
        Assert.Equal(orders,_db.Orders.Count()); Assert.Equal(details,_db.OrderDetails.Count());
    }

    [Fact]
    public void GuestCheckout_DoesNotAttachOrRewardAnUnrelatedMember()
    {
        int points=Points;
        var receipt=_checkout.Checkout("cashier01",null,null,Cart());
        Assert.Null(_db.Orders.Single(o=>o.OrderId==receipt.OrderId).CustomerId);
        Assert.Equal(0,receipt.RewardPointsAdded); Assert.Equal(points,Points);
        _checkout.Cancel(receipt.OrderId);
        Assert.Equal(points,Points);
    }

    [Fact]
    public void Cancel_RestoresStockPointsAndRankExactlyOnce()
    {
        _db.Database.ExecuteSqlRaw("UPDATE Customers SET RewardPoints=199,MembershipRank='Chuẩn' WHERE CustomerId=1");
        int stock=Stock;
        var receipt=_checkout.Checkout("cashier01",1,null,Cart());
        Assert.Equal(200,Points);
        Assert.Equal("Bạc",_db.Customers.AsNoTracking().Single(c=>c.CustomerId==1).MembershipRank);
        _checkout.Cancel(receipt.OrderId);
        Assert.Equal(stock,Stock); Assert.Equal(199,Points);
        Assert.Equal("Chuẩn",_db.Customers.AsNoTracking().Single(c=>c.CustomerId==1).MembershipRank);
        Assert.Throws<InvalidOperationException>(()=>_checkout.Cancel(receipt.OrderId));
        Assert.Equal(stock,Stock); Assert.Equal(199,Points);
        Assert.False(_db.OrderDetails.Any(d=>d.OrderId==receipt.OrderId));
    }

    [Fact]
    public async Task QuickReport_UsesActualDayTotalsAndQuantitiesRatherThanPrice()
    {
        var report=new ReportsController(_db);
        var result=Json(await report.GetQuickReport("2026-10-01"));
        Assert.Equal(3,result.GetProperty("TotalOrders").GetInt32());
        Assert.Equal(247000m,result.GetProperty("TotalRevenue").GetDecimal());
        Assert.Equal(_db.Products.Single(p=>p.ProductId==13).ProductName,result.GetProperty("BestSeller").GetString());
        var empty=Json(await report.GetQuickReport("2020-01-01"));
        Assert.Equal(0,empty.GetProperty("TotalOrders").GetInt32());
        Assert.Equal(0,empty.GetProperty("TotalRevenue").GetDecimal());
    }

    [Fact]
    public void Login_DatabaseAccountRejectsCommonPasswordAndMissingFixtureAccount()
    {
        var auth=new AuthController(_config,_db);
        Assert.IsType<UnauthorizedObjectResult>(auth.Login(new(){Username="admin",Password="123456"}));
        Assert.IsType<UnauthorizedObjectResult>(auth.Login(new(){Username="admin01",Password="123456"}));
        Assert.IsType<OkObjectResult>(auth.Login(new(){Username="admin",Password="admin@123"}));
    }

    [Fact]
    public void LegacyRoute_UsesSameTransactionAndServerPrice()
    {
        var controller=new OrdersController(_db,_checkout);
        var result=Json(controller.Checkout(new(){CashierUsername="cashier01",Items=new(){new(){ProductId=1,Quantity=1,UnitPrice=0}}}));
        Assert.Equal(12500m,result.GetProperty("totalAmount").GetDecimal());
        Assert.Null(_db.Orders.Single(o=>o.OrderId==result.GetProperty("orderId").GetInt32()).CustomerId);
    }

    [Fact]
    public void AdoLesson1_ExecutesAllThreeOperationsOnSqlite()
    {
        var demo=new HeThongBanLeThienAn.Tiet1ConsoleDemo.AdoNetTiet1Demo(new DbConnectionFactory(_config));
        Assert.Equal(100,demo.CountTotalProducts());
        Assert.Equal(1,demo.UpdateProductStock(1,42));
        Assert.Equal(42,demo.ReadAllProducts().Single(p=>p.Id==1).Stock);
        Assert.Throws<ArgumentOutOfRangeException>(()=>demo.UpdateProductStock(1,-1));
    }

    [Fact]
    public void NewDatabase_DoesNotCreateFakeCustomerProfiles()
    {
        using var connection=new SqliteConnection("Data Source=:memory:"); connection.Open();
        using var db=new SupermarketDbContext(new DbContextOptionsBuilder<SupermarketDbContext>().UseSqlite(connection).Options);
        db.Database.Migrate();
        Assert.Empty(db.Customers);
        Assert.All(db.Orders.ToList(),o=> { Assert.Null(o.CustomerId); Assert.Equal(0,o.RewardPoints); });
    }

    public void Dispose() { _db.Dispose(); _connection.Dispose(); }
}
