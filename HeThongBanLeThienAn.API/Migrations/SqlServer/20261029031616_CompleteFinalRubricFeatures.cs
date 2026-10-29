using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeThongBanLeThienAn.API.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class CompleteFinalRubricFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "RedeemedPoints",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                columns: new[] { "DiscountAmount", "RedeemedPoints" },
                values: new object[] { 0m, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                columns: new[] { "DiscountAmount", "RedeemedPoints" },
                values: new object[] { 0m, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 3,
                columns: new[] { "DiscountAmount", "RedeemedPoints" },
                values: new object[] { 0m, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 4,
                columns: new[] { "DiscountAmount", "RedeemedPoints" },
                values: new object[] { 0m, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 5,
                columns: new[] { "DiscountAmount", "RedeemedPoints" },
                values: new object[] { 0m, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 6,
                columns: new[] { "DiscountAmount", "RedeemedPoints" },
                values: new object[] { 0m, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 7,
                columns: new[] { "DiscountAmount", "RedeemedPoints" },
                values: new object[] { 0m, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 8,
                columns: new[] { "DiscountAmount", "RedeemedPoints" },
                values: new object[] { 0m, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 9,
                columns: new[] { "DiscountAmount", "RedeemedPoints" },
                values: new object[] { 0m, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 10,
                columns: new[] { "DiscountAmount", "RedeemedPoints" },
                values: new object[] { 0m, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 11,
                columns: new[] { "DiscountAmount", "RedeemedPoints" },
                values: new object[] { 0m, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 12,
                columns: new[] { "DiscountAmount", "RedeemedPoints" },
                values: new object[] { 0m, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 13,
                columns: new[] { "DiscountAmount", "RedeemedPoints" },
                values: new object[] { 0m, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 14,
                columns: new[] { "DiscountAmount", "RedeemedPoints" },
                values: new object[] { 0m, 0 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 15,
                columns: new[] { "DiscountAmount", "RedeemedPoints" },
                values: new object[] { 0m, 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Vương Nguyễn Trường Hưng", "PBKDF2$120000$fNDfJNL02Yi7roCaKIU0ig==$bFig0dj3BTLMsOZMhdOg6rtg8XBfiEN4lxtuhtbpKjg=" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Trần Minh Đức", "PBKDF2$120000$nk0OWyFDop8TPfATE30SEQ==$uPEDH4XjJUCMGFgcHRQ1SdK/7d+oXI7GIxdfrWKiLNE=" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Lê Thị Thảo Trang", "PBKDF2$120000$D+IxvDiAJ9TSI5knsm4m+w==$S7xyaGEyoaW8CvxuzvJzv0+EyCZR39AVhyO896uNMH8=" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Nguyễn Phạm Hoàng Yến", "PBKDF2$120000$D+IxvDiAJ9TSI5knsm4m+w==$S7xyaGEyoaW8CvxuzvJzv0+EyCZR39AVhyO896uNMH8=" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Phan Thanh Tùng", "PBKDF2$120000$D+IxvDiAJ9TSI5knsm4m+w==$S7xyaGEyoaW8CvxuzvJzv0+EyCZR39AVhyO896uNMH8=" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Võ Thị Cẩm Nhung", "PBKDF2$120000$D+IxvDiAJ9TSI5knsm4m+w==$S7xyaGEyoaW8CvxuzvJzv0+EyCZR39AVhyO896uNMH8=" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Đặng Văn Hữu", "PBKDF2$120000$D+IxvDiAJ9TSI5knsm4m+w==$S7xyaGEyoaW8CvxuzvJzv0+EyCZR39AVhyO896uNMH8=" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Bùi Ngọc Quỳnh Nga", "PBKDF2$120000$D+IxvDiAJ9TSI5knsm4m+w==$S7xyaGEyoaW8CvxuzvJzv0+EyCZR39AVhyO896uNMH8=" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 9,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Hồ Nhật Huy", "PBKDF2$120000$D+IxvDiAJ9TSI5knsm4m+w==$S7xyaGEyoaW8CvxuzvJzv0+EyCZR39AVhyO896uNMH8=" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 10,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Đỗ Trọng Nhân", "PBKDF2$120000$NOHeTrckKdOS9INBWt3sJw==$1Cg/dfN4Tx9uxuhWGNMlFDwKNquQxdkQ+aqpeU279cE=" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 11,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Ngô Thành Danh", "PBKDF2$120000$NOHeTrckKdOS9INBWt3sJw==$1Cg/dfN4Tx9uxuhWGNMlFDwKNquQxdkQ+aqpeU279cE=" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 12,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Dương Quốc Thắng", "PBKDF2$120000$NOHeTrckKdOS9INBWt3sJw==$1Cg/dfN4Tx9uxuhWGNMlFDwKNquQxdkQ+aqpeU279cE=" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 13,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Trịnh Kim Liên", "PBKDF2$120000$D+IxvDiAJ9TSI5knsm4m+w==$S7xyaGEyoaW8CvxuzvJzv0+EyCZR39AVhyO896uNMH8=" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 14,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Lương Minh Khôi", "PBKDF2$120000$D+IxvDiAJ9TSI5knsm4m+w==$S7xyaGEyoaW8CvxuzvJzv0+EyCZR39AVhyO896uNMH8=" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 15,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Mai Văn Hải", "PBKDF2$120000$pBnAQ5ACtef9KhgHUaeRwA==$04ueyv+6TONSsazbvcds4KFrLaqSH3u2koqHqQjhFxY=" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RedeemedPoints",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Customers");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Nhân viên mẫu 01", "admin@123" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Nhân viên mẫu 02", "manager@123" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Nhân viên mẫu 03", "cashier@123" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Nhân viên mẫu 04", "cashier@123" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Nhân viên mẫu 05", "cashier@123" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Nhân viên mẫu 06", "cashier@123" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Nhân viên mẫu 07", "cashier@123" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Nhân viên mẫu 08", "cashier@123" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 9,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Nhân viên mẫu 09", "cashier@123" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 10,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Nhân viên mẫu 10", "wh@123" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 11,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Nhân viên mẫu 11", "wh@123" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 12,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Nhân viên mẫu 12", "wh@123" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 13,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Nhân viên mẫu 13", "cashier@123" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 14,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Nhân viên mẫu 14", "cashier@123" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 15,
                columns: new[] { "FullName", "PasswordHash" },
                values: new object[] { "Nhân viên mẫu 15", "lock@123" });
        }
    }
}
