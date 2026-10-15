using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Controllers;
using MiniSupermarket.API.Data;
using System.Threading.Tasks;
using Xunit;

namespace HeThongBanLeThienAn.Tests
{
    public class ReportsControllerTests
    {
        [Fact]
        public async Task GetQuickReport_WithSqliteDatabase_ExecutesWithoutException()
        {
            // Test specifically with SQLite to ensure EF Core translates LINQ OrderBy without decimal exception
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<SupermarketDbContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new SupermarketDbContext(options))
                {
                    context.Database.EnsureCreated(); // Seeds 15 sample products
                }

                using (var context = new SupermarketDbContext(options))
                {
                    var controller = new ReportsController(context);

                    // Act
                    var result = await controller.GetQuickReport("2026-10-08") as OkObjectResult;

                    // Assert
                    Assert.NotNull(result);
                    Assert.Equal(200, result.StatusCode);
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
