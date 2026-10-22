using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeThongBanLeThienAn.API.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class CreateReportingStoredProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                CREATE OR ALTER PROCEDURE dbo.sp_GetRevenueByCashier
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT CashierUsername,
                           COUNT(OrderId) AS TotalOrders,
                           SUM(FinalAmount) AS TotalRevenue,
                           SUM(RewardPoints) AS TotalPointsAwarded
                    FROM Orders
                    GROUP BY CashierUsername
                    ORDER BY TotalRevenue DESC;
                END
                """);

            migrationBuilder.Sql("""
                CREATE OR ALTER PROCEDURE dbo.sp_GetRevenueByDateRange
                    @FromDate DATETIME2,
                    @ToDate DATETIME2
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT CashierUsername,
                           COUNT(OrderId) AS TotalOrders,
                           SUM(FinalAmount) AS TotalRevenue,
                           SUM(RewardPoints) AS TotalPointsAwarded
                    FROM Orders
                    WHERE CreatedDate >= @FromDate AND CreatedDate < @ToDate
                    GROUP BY CashierUsername
                    ORDER BY TotalRevenue DESC;
                END
                """);

            migrationBuilder.Sql("""
                CREATE OR ALTER PROCEDURE dbo.sp_GetTop5SellingProducts
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT TOP (5)
                           p.ProductId,
                           p.Barcode,
                           p.ProductName,
                           p.Price,
                           SUM(od.Quantity) AS TotalSoldQuantity,
                           SUM(od.LineTotal) AS TotalSalesAmount
                    FROM OrderDetails AS od
                    INNER JOIN Products AS p ON od.ProductId = p.ProductId
                    GROUP BY p.ProductId, p.Barcode, p.ProductName, p.Price
                    ORDER BY TotalSoldQuantity DESC, TotalSalesAmount DESC;
                END
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.sp_GetTop5SellingProducts;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.sp_GetRevenueByDateRange;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.sp_GetRevenueByCashier;");
        }
    }
}
