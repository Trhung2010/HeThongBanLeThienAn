using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeThongBanLeThienAn.API.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class DailyRevenueChart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                CREATE OR ALTER PROCEDURE dbo.sp_GetDailyRevenue
                    @FromDate datetime2, @ToDate datetime2
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT CAST(CreatedDate AS date) AS RevenueDate,
                           COUNT(*) AS TotalOrders, SUM(FinalAmount) AS TotalRevenue
                    FROM dbo.Orders
                    WHERE CreatedDate >= @FromDate AND CreatedDate < @ToDate
                    GROUP BY CAST(CreatedDate AS date)
                    ORDER BY RevenueDate;
                END
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.sp_GetDailyRevenue;");
        }
    }
}
