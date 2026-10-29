using System.Collections.Generic;

namespace MiniSupermarket.API.Data
{
    public class UserAccount
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // Admin, Cashier, Warehouse
        public bool IsActive { get; set; } = true;
    }

    public static class UserDataStore
    {
        public static List<UserAccount> Users { get; } = new List<UserAccount>
        {
            new UserAccount { Id = 1, Username = "admin01", PasswordHash = PasswordFixtureHash, FullName = "Nguyễn Quản Trị", Role = "Admin", IsActive = true },
            new UserAccount { Id = 2, Username = "admin02", PasswordHash = PasswordFixtureHash, FullName = "Trần Giám Đốc", Role = "Admin", IsActive = true },
            new UserAccount { Id = 3, Username = "cashier01", PasswordHash = PasswordFixtureHash, FullName = "Lê Thu Ngân", Role = "Cashier", IsActive = true },
            new UserAccount { Id = 4, Username = "cashier02", PasswordHash = PasswordFixtureHash, FullName = "Phạm Bán Hàng", Role = "Cashier", IsActive = true },
            new UserAccount { Id = 5, Username = "cashier03", PasswordHash = PasswordFixtureHash, FullName = "Hoàng Thu Ngân", Role = "Cashier", IsActive = true },
            new UserAccount { Id = 6, Username = "cashier04", PasswordHash = PasswordFixtureHash, FullName = "Vũ Thị Quầy", Role = "Cashier", IsActive = true },
            new UserAccount { Id = 7, Username = "cashier05", PasswordHash = PasswordFixtureHash, FullName = "Đỗ Bán Lẻ", Role = "Cashier", IsActive = true },
            new UserAccount { Id = 8, Username = "ware01", PasswordHash = PasswordFixtureHash, FullName = "Ngô Quản Kho", Role = "Warehouse", IsActive = true },
            new UserAccount { Id = 9, Username = "ware02", PasswordHash = PasswordFixtureHash, FullName = "Bùi Kiểm Kê", Role = "Warehouse", IsActive = true },
            new UserAccount { Id = 10, Username = "ware03", PasswordHash = PasswordFixtureHash, FullName = "Dương Thủ Kho", Role = "Warehouse", IsActive = true },
            new UserAccount { Id = 11, Username = "ware04", PasswordHash = PasswordFixtureHash, FullName = "Lý Nhập Hàng", Role = "Warehouse", IsActive = true },
            new UserAccount { Id = 12, Username = "admin_backup", PasswordHash = PasswordFixtureHash, FullName = "Đặng Hỗ Trợ", Role = "Admin", IsActive = true },
            new UserAccount { Id = 13, Username = "cashier06", PasswordHash = PasswordFixtureHash, FullName = "Hồ Ca Chiều", Role = "Cashier", IsActive = true },
            new UserAccount { Id = 14, Username = "ware05", PasswordHash = PasswordFixtureHash, FullName = "Trương Vận Chuyển", Role = "Warehouse", IsActive = true },
            new UserAccount { Id = 15, Username = "supervisor", PasswordHash = PasswordFixtureHash, FullName = "Mai Giám Sát", Role = "Admin", IsActive = true },
            // Giữ lại 2 tài khoản mặc định cũ để tương thích bài test
            new UserAccount { Id = 16, Username = "admin", PasswordHash = PasswordFixtureHash, FullName = "Quản Trị Mặc Định", Role = "Admin", IsActive = true },
            new UserAccount { Id = 17, Username = "cashier", PasswordHash = PasswordFixtureHash, FullName = "Thu Ngân Mặc Định", Role = "Cashier", IsActive = true }
        };

        private const string PasswordFixtureHash = "PBKDF2$120000$QVAM4Xb3YVLD1S6pdYoVZw==$oEH0TzrZ7j0wpssuUuwUcmVIkCLGo3bVIleOSpJoOLk=";
    }
}
