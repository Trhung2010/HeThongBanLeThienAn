using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace HeThongBanLeThienAn.API.Migrations;
public partial class CompleteBuoi5Sqlite : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Seed removal must never DeleteData by ID alone: those IDs may now hold real profiles.
        // Keep invoice amounts/details and preserve every non-demo customer and relationship.
        migrationBuilder.Sql(@"UPDATE Orders SET CustomerId = NULL, RewardPoints = 0
            WHERE CustomerId IN (SELECT CustomerId FROM Customers
                WHERE CustomerId BETWEEN 1 AND 15
                AND CustomerName = 'Khách mẫu ' || printf('%02d',CustomerId)
                AND PhoneNumber = 'DEMO-KH-' || printf('%03d',CustomerId));");
        migrationBuilder.Sql(@"DELETE FROM Customers
            WHERE CustomerId BETWEEN 1 AND 15
            AND CustomerName = 'Khách mẫu ' || printf('%02d',CustomerId)
            AND PhoneNumber = 'DEMO-KH-' || printf('%03d',CustomerId);");
    }
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        throw new NotSupportedException("Không thể tái tạo hồ sơ mẫu và quan hệ đã dọn. Khôi phục bản sao SQLite nếu cần quay lại.");
    }
}
