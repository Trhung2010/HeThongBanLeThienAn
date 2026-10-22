using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeThongBanLeThienAn.API.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class RenameSeedProductDataSource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                SELECT ProductId, DataSource INTO #PreservedProductDataSources
                FROM Products
                WHERE ProductId BETWEEN 1 AND 100
                  AND DataSource <> N'Tham khảo thực hành; chưa xác minh từ cửa hàng';
                """);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 19,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 20,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 22,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 23,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 24,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 25,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 26,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 27,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 28,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 29,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 30,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 31,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 32,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 33,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 34,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 35,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 36,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 37,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 38,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 39,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 40,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 41,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 42,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 43,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 44,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 45,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 46,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 47,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 48,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 49,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 50,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 51,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 52,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 53,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 54,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 55,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 56,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 57,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 58,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 59,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 60,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 61,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 62,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 63,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 64,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 65,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 66,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 67,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 68,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 69,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 70,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 71,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 72,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 73,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 74,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 75,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 76,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 77,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 78,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 79,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 80,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 81,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 82,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 83,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 84,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 85,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 86,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 87,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 88,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 89,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 90,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 91,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 92,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 93,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 94,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 95,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 96,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 97,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 98,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 99,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 100,
                column: "DataSource",
                value: "Kho tổng Thiên Ân");

            migrationBuilder.Sql("""
                UPDATE product
                SET product.DataSource = preserved.DataSource
                FROM Products AS product
                INNER JOIN #PreservedProductDataSources AS preserved
                    ON preserved.ProductId = product.ProductId;
                DROP TABLE #PreservedProductDataSources;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 16,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 17,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 18,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 19,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 20,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 22,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 23,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 24,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 25,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 26,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 27,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 28,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 29,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 30,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 31,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 32,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 33,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 34,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 35,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 36,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 37,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 38,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 39,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 40,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 41,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 42,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 43,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 44,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 45,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 46,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 47,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 48,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 49,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 50,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 51,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 52,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 53,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 54,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 55,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 56,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 57,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 58,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 59,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 60,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 61,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 62,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 63,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 64,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 65,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 66,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 67,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 68,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 69,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 70,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 71,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 72,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 73,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 74,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 75,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 76,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 77,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 78,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 79,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 80,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 81,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 82,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 83,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 84,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 85,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 86,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 87,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 88,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 89,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 90,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 91,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 92,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 93,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 94,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 95,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 96,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 97,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 98,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 99,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 100,
                column: "DataSource",
                value: "Tham khảo thực hành; chưa xác minh từ cửa hàng");
        }
    }
}
