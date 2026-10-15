using System;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    /// <summary>
    /// Ngữ cảnh cơ sở dữ liệu đại diện cho phiên làm việc với SQL Server / SQLite
    /// Quản lý 6 thực thể và cấu hình Data Seeding đầy đủ cho hệ thống Siêu thị Mini
    /// </summary>
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options) :
            base(options) { }

        // Khai báo 6 bảng dữ liệu của hệ thống
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================================================================
            // A. CẤU HÌNH RÀNG BUỘC CHỈ MỤC DUY NHẤT (UNIQUE INDEX)
            // =========================================================================
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.CategoryName).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(p => p.Barcode).IsUnique();
            modelBuilder.Entity<Customer>().HasIndex(c => c.PhoneNumber).IsUnique();
            modelBuilder.Entity<Order>().HasIndex(o => o.OrderCode).IsUnique();

            // =========================================================================
            // B. DATA SEEDING CHO 6 BẢNG (MỖI BẢNG TỐI THIỂU 15 DÒNG)
            // =========================================================================

            // 1. DATA SEEDING: BẢNG USERS (15 Nhân viên thuộc 3 bộ phận: Admin, Cashier, Warehouse)
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "admin", PasswordHash = "admin@123", FullName = "Nguyễn Quản Trị", Role = "Admin", IsActive = true },
                new User { UserId = 2, Username = "manager01", PasswordHash = "manager@123", FullName = "Trần Cửa Hàng Trưởng", Role = "Admin", IsActive = true },
                new User { UserId = 3, Username = "cashier01", PasswordHash = "cashier@123", FullName = "Lê Thu Ngân 1", Role = "Cashier", IsActive = true },
                new User { UserId = 4, Username = "cashier02", PasswordHash = "cashier@123", FullName = "Phạm Thu Ngân 2", Role = "Cashier", IsActive = true },
                new User { UserId = 5, Username = "cashier03", PasswordHash = "cashier@123", FullName = "Hoàng Thu Ngân 3", Role = "Cashier", IsActive = true },
                new User { UserId = 6, Username = "cashier04", PasswordHash = "cashier@123", FullName = "Vũ Thu Ngân 4", Role = "Cashier", IsActive = true },
                new User { UserId = 7, Username = "cashier05", PasswordHash = "cashier@123", FullName = "Đỗ Thu Ngân 5", Role = "Cashier", IsActive = true },
                new User { UserId = 8, Username = "cashier06", PasswordHash = "cashier@123", FullName = "Ngô Thu Ngân 6", Role = "Cashier", IsActive = true },
                new User { UserId = 9, Username = "cashier07", PasswordHash = "cashier@123", FullName = "Bùi Thu Ngân 7", Role = "Cashier", IsActive = true },
                new User { UserId = 10, Username = "warehouse01", PasswordHash = "wh@123", FullName = "Đặng Thủ Kho 1", Role = "Warehouse", IsActive = true },
                new User { UserId = 11, Username = "warehouse02", PasswordHash = "wh@123", FullName = "Trương Thủ Kho 2", Role = "Warehouse", IsActive = true },
                new User { UserId = 12, Username = "warehouse03", PasswordHash = "wh@123", FullName = "Lý Kiểm Kê", Role = "Warehouse", IsActive = true },
                new User { UserId = 13, Username = "cashier08", PasswordHash = "cashier@123", FullName = "Mai Thu Ngân 8", Role = "Cashier", IsActive = true },
                new User { UserId = 14, Username = "cashier09", PasswordHash = "cashier@123", FullName = "Dương Thu Ngân 9", Role = "Cashier", IsActive = true },
                new User { UserId = 15, Username = "staff_lock", PasswordHash = "lock@123", FullName = "Hồ Nhân Viên Nghỉ Việc", Role = "Cashier", IsActive = false }
            );

            // 2. DATA SEEDING: BẢNG CATEGORIES (15 Nhóm hàng siêu thị thực tế)
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Bánh kẹo & Đồ ăn vặt", Description = "Snack khoai tây, bánh quy bơ, kẹo trái cây" },
                new Category { CategoryId = 2, CategoryName = "Nước giải khát & Trà", Description = "Nước ngọt lon, nước khoáng thiên nhiên, trà xanh đóng chai" },
                new Category { CategoryId = 3, CategoryName = "Sữa & Sản phẩm từ sữa", Description = "Sữa tươi tiệt trùng, sữa đặc, sữa chua ăn, phô mai lát" },
                new Category { CategoryId = 4, CategoryName = "Mì gói & Thực phẩm ăn liền", Description = "Mì ăn liền, hủ tiếu, phở khô, cháo gói dinh dưỡng" },
                new Category { CategoryId = 5, CategoryName = "Gia vị & Dầu ăn", Description = "Nước mắm cá cơm, hạt nêm, dầu ăn nguyên chất, muối i-ốt" },
                new Category { CategoryId = 6, CategoryName = "Đồ hộp & Chế biến sẵn", Description = "Cá hộp sốt cà, thịt heo hầm hộp, pate gan, xúc xích ăn liền" },
                new Category { CategoryId = 7, CategoryName = "Hóa phẩm tẩy rửa", Description = "Nước giặt xả, nước lau sàn nhà, nước rửa chén, bột tẩy" },
                new Category { CategoryId = 8, CategoryName = "Chăm sóc cá nhân", Description = "Dầu gội đầu, sữa tắm dưỡng ẩm, kem đánh răng, xà phòng" },
                new Category { CategoryId = 9, CategoryName = "Đồ gia dụng tiện ích", Description = "Màng bọc thực phẩm, túi rác sinh học, khăn giấy rút, pin tiểu" },
                new Category { CategoryId = 10, CategoryName = "Rau củ tươi sạch", Description = "Cà chua bi, rau cải xanh, khoai tây, cà rốt Đà Lạt" },
                new Category { CategoryId = 11, CategoryName = "Thịt tươi & Thủy sản", Description = "Thịt ba chỉ heo, ức gà phi lê, cá phi lê tươi sống" },
                new Category { CategoryId = 12, CategoryName = "Thực phẩm đông lạnh", Description = "Bánh bao nhân thịt, há cảo hấp, cá viên, bò viên chiên" },
                new Category { CategoryId = 13, CategoryName = "Cà phê & Ngũ cốc", Description = "Cà phê hòa tan 3in1, bột yến mạch, bột ngũ cốc dinh dưỡng" },
                new Category { CategoryId = 14, CategoryName = "Văn phòng phẩm mini", Description = "Bút bi bấm, vở viết học sinh, kéo cắt giấy văn phòng" },
                new Category { CategoryId = 15, CategoryName = "Vật tư đóng gói POS", Description = "Bao xốp quầy thu ngân, giấy in bill nhiệt k80, tem dán nhãn" }
            );

            // 3. DATA SEEDING: BẢNG PRODUCTS (20 Mặt hàng thiết yếu phủ các nhóm hàng)
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Barcode = "893456789001", ProductName = "Snack Khoai Tây O'Star Gói 60g", Price = 12500m, StockQuantity = 150, CategoryId = 1 },
                new Product { ProductId = 2, Barcode = "893456789002", ProductName = "Bánh Quy Bơ Danisa Hộp Thiếc 454g", Price = 135000m, StockQuantity = 40, CategoryId = 1 },
                new Product { ProductId = 3, Barcode = "893456789003", ProductName = "Kẹo Dẻo Chupa Chups Hương Trái Cây 90g", Price = 15000m, StockQuantity = 120, CategoryId = 1 },
                new Product { ProductId = 4, Barcode = "893456789004", ProductName = "Bánh Chocopie Orion Hộp 12 Cái", Price = 54000m, StockQuantity = 60, CategoryId = 1 },
                new Product { ProductId = 5, Barcode = "893456789005", ProductName = "Nước Ngọt Coca Cola Lon 320ml", Price = 10000m, StockQuantity = 320, CategoryId = 2 },
                new Product { ProductId = 6, Barcode = "893456789006", ProductName = "Nước Khoáng Thiên Nhiên Lavie Chai 500ml", Price = 6000m, StockQuantity = 200, CategoryId = 2 },
                new Product { ProductId = 7, Barcode = "893456789007", ProductName = "Trà Xanh Không Độ Chai 455ml", Price = 11000m, StockQuantity = 180, CategoryId = 2 },
                new Product { ProductId = 8, Barcode = "893456789008", ProductName = "Nước Tăng Lực Red Bull Lon 250ml", Price = 14500m, StockQuantity = 150, CategoryId = 2 },
                new Product { ProductId = 9, Barcode = "893456789009", ProductName = "Sữa Tươi Tiệt Trùng Vinamilk Có Đường 1L", Price = 34500m, StockQuantity = 85, CategoryId = 3 },
                new Product { ProductId = 10, Barcode = "893456789010", ProductName = "Phô Mai Con Bò Cười Hộp 8 Miếng", Price = 39000m, StockQuantity = 60, CategoryId = 3 },
                new Product { ProductId = 11, Barcode = "893456789011", ProductName = "Sữa Chua Ăn Vinamilk Có Đường Lốc 4 Hộp", Price = 28000m, StockQuantity = 95, CategoryId = 3 },
                new Product { ProductId = 12, Barcode = "893456789012", ProductName = "Sữa Đặc Có Đường Ông Thọ Lon 380g", Price = 26000m, StockQuantity = 110, CategoryId = 3 },
                new Product { ProductId = 13, Barcode = "893456789013", ProductName = "Mì Tôm Hảo Hảo Chua Cay Gói 75g", Price = 4500m, StockQuantity = 500, CategoryId = 4 },
                new Product { ProductId = 14, Barcode = "893456789014", ProductName = "Phở Bò Đệ Nhất Gói 68g", Price = 8500m, StockQuantity = 140, CategoryId = 4 },
                new Product { ProductId = 15, Barcode = "893456789015", ProductName = "Mì Trộn Tương Đen Koreno Gói 115g", Price = 14000m, StockQuantity = 80, CategoryId = 4 },
                new Product { ProductId = 16, Barcode = "893456789016", ProductName = "Dầu Ăn Simply Nguyên Chất Can 2L", Price = 125000m, StockQuantity = 45, CategoryId = 5 },
                new Product { ProductId = 17, Barcode = "893456789017", ProductName = "Nước Mắm Nam Ngư Đệ Nhị Chai 900ml", Price = 30000m, StockQuantity = 90, CategoryId = 5 },
                new Product { ProductId = 18, Barcode = "893456789018", ProductName = "Hạt Nêm Knorr Từ Thịt Thăn Gói 400g", Price = 36000m, StockQuantity = 75, CategoryId = 5 },
                new Product { ProductId = 19, Barcode = "893456789019", ProductName = "Cá Nục Sốt Cà Ba Cô Gái Hộp 155g", Price = 19000m, StockQuantity = 130, CategoryId = 6 },
                new Product { ProductId = 20, Barcode = "893456789020", ProductName = "Nước Rửa Chén Sunlight Chanh Chai 750g", Price = 29000m, StockQuantity = 80, CategoryId = 7 }
            );

            // 4. DATA SEEDING: BẢNG CUSTOMERS (15 Khách hàng thân thiết mẫu tại TP.HCM)
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, CustomerName = "Trần Văn Hoàng", PhoneNumber = "0903123456", Address = "123 Lê Lợi, Quận 1, TPHCM", RewardPoints = 120, MembershipRank = "Bạc" },
                new Customer { CustomerId = 2, CustomerName = "Nguyễn Thị Kim Loan", PhoneNumber = "0918234567", Address = "45 Điện Biên Phủ, Quận Bình Thạnh, TPHCM", RewardPoints = 350, MembershipRank = "Vàng" },
                new Customer { CustomerId = 3, CustomerName = "Lê Minh Trí", PhoneNumber = "0977345678", Address = "88 Nam Kỳ Khởi Nghĩa, Quận 3, TPHCM", RewardPoints = 45, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 4, CustomerName = "Phạm Hồng Nhung", PhoneNumber = "0988456789", Address = "102 Quang Trung, Quận Gò Vấp, TPHCM", RewardPoints = 520, MembershipRank = "Bạch Kim" },
                new Customer { CustomerId = 5, CustomerName = "Võ Thành Nam", PhoneNumber = "0909567890", Address = "15 Phan Xích Long, Quận Phú Nhuận, TPHCM", RewardPoints = 80, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 6, CustomerName = "Đỗ Thị Mỹ Duyên", PhoneNumber = "0933678901", Address = "234 Hoàng Văn Thụ, Quận Tân Bình, TPHCM", RewardPoints = 210, MembershipRank = "Bạc" },
                new Customer { CustomerId = 7, CustomerName = "Hoàng Quốc Bảo", PhoneNumber = "0944789012", Address = "312 Ba Tháng Hai, Quận 10, TPHCM", RewardPoints = 15, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 8, CustomerName = "Bùi Lan Hương", PhoneNumber = "0966890123", Address = "56 Nguyễn Thị Thập, Quận 7, TPHCM", RewardPoints = 410, MembershipRank = "Vàng" },
                new Customer { CustomerId = 9, CustomerName = "Đặng Tuấn Kiệt", PhoneNumber = "0922901234", Address = "79 Kinh Dương Vương, Quận Bình Tân, TPHCM", RewardPoints = 95, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 10, CustomerName = "Ngô Thanh Vân", PhoneNumber = "0971012345", Address = "18 Hoàng Diệu, Quận 4, TPHCM", RewardPoints = 650, MembershipRank = "Bạch Kim" },
                new Customer { CustomerId = 11, CustomerName = "Trương Đình Trọng", PhoneNumber = "0982123456", Address = "405 Lê Văn Khương, Quận 12, TPHCM", RewardPoints = 30, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 12, CustomerName = "Lý Mỹ Lệ", PhoneNumber = "0904234567", Address = "123 Long Thuận, TP. Thủ Đức, TPHCM", RewardPoints = 180, MembershipRank = "Bạc" },
                new Customer { CustomerId = 13, CustomerName = "Vũ Đức Thịnh", PhoneNumber = "0915345678", Address = "67 Lũy Bán Bích, Quận Tân Phú, TPHCM", RewardPoints = 290, MembershipRank = "Bạc" },
                new Customer { CustomerId = 14, CustomerName = "Phan Bích Thủy", PhoneNumber = "0936456789", Address = "142 Phạm Thế Hiển, Quận 8, TPHCM", RewardPoints = 480, MembershipRank = "Vàng" },
                new Customer { CustomerId = 15, CustomerName = "Dương Gia Huy", PhoneNumber = "0977567890", Address = "89 An Dương Vương, Quận 5, TPHCM", RewardPoints = 0, MembershipRank = "Chuẩn" }
            );

            // 5. DATA SEEDING: BẢNG ORDERS (15 Hóa đơn bán lẻ phát sinh tại quầy)
            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, OrderCode = "HD20261001-001", CreatedDate = new DateTime(2026, 10, 1, 8, 30, 0), CustomerId = 1, CashierUsername = "cashier01", TotalItems = 3, FinalAmount = 57000m, RewardPoints = 5 },
                new Order { OrderId = 2, OrderCode = "HD20261001-002", CreatedDate = new DateTime(2026, 10, 1, 9, 15, 0), CustomerId = 2, CashierUsername = "cashier01", TotalItems = 2, FinalAmount = 145000m, RewardPoints = 14 },
                new Order { OrderId = 3, OrderCode = "HD20261001-003", CreatedDate = new DateTime(2026, 10, 1, 10, 0, 0), CustomerId = 3, CashierUsername = "cashier02", TotalItems = 10, FinalAmount = 45000m, RewardPoints = 4 },
                new Order { OrderId = 4, OrderCode = "HD20261002-001", CreatedDate = new DateTime(2026, 10, 2, 8, 45, 0), CustomerId = 4, CashierUsername = "cashier02", TotalItems = 4, FinalAmount = 138000m, RewardPoints = 13 },
                new Order { OrderId = 5, OrderCode = "HD20261002-002", CreatedDate = new DateTime(2026, 10, 2, 11, 20, 0), CustomerId = 5, CashierUsername = "cashier03", TotalItems = 2, FinalAmount = 69000m, RewardPoints = 6 },
                new Order { OrderId = 6, OrderCode = "HD20261003-001", CreatedDate = new DateTime(2026, 10, 3, 14, 10, 0), CustomerId = 6, CashierUsername = "cashier03", TotalItems = 3, FinalAmount = 53500m, RewardPoints = 5 },
                new Order { OrderId = 7, OrderCode = "HD20261003-002", CreatedDate = new DateTime(2026, 10, 3, 16, 40, 0), CustomerId = 7, CashierUsername = "cashier04", TotalItems = 5, FinalAmount = 30000m, RewardPoints = 3 },
                new Order { OrderId = 8, OrderCode = "HD20261004-001", CreatedDate = new DateTime(2026, 10, 4, 9, 05, 0), CustomerId = 8, CashierUsername = "cashier04", TotalItems = 2, FinalAmount = 155000m, RewardPoints = 15 },
                new Order { OrderId = 9, OrderCode = "HD20261004-002", CreatedDate = new DateTime(2026, 10, 4, 13, 30, 0), CustomerId = 9, CashierUsername = "cashier05", TotalItems = 4, FinalAmount = 48500m, RewardPoints = 4 },
                new Order { OrderId = 10, OrderCode = "HD20261005-001", CreatedDate = new DateTime(2026, 10, 5, 10, 50, 0), CustomerId = 10, CashierUsername = "cashier05", TotalItems = 6, FinalAmount = 168000m, RewardPoints = 16 },
                new Order { OrderId = 11, OrderCode = "HD20261005-002", CreatedDate = new DateTime(2026, 10, 5, 17, 15, 0), CustomerId = 11, CashierUsername = "cashier06", TotalItems = 8, FinalAmount = 36000m, RewardPoints = 3 },
                new Order { OrderId = 12, OrderCode = "HD20261006-001", CreatedDate = new DateTime(2026, 10, 6, 8, 20, 0), CustomerId = 12, CashierUsername = "cashier06", TotalItems = 2, FinalAmount = 108000m, RewardPoints = 10 },
                new Order { OrderId = 13, OrderCode = "HD20261006-002", CreatedDate = new DateTime(2026, 10, 6, 15, 00, 0), CustomerId = 13, CashierUsername = "cashier07", TotalItems = 3, FinalAmount = 78000m, RewardPoints = 7 },
                new Order { OrderId = 14, OrderCode = "HD20261007-001", CreatedDate = new DateTime(2026, 10, 7, 12, 10, 0), CustomerId = 14, CashierUsername = "cashier07", TotalItems = 1, FinalAmount = 125000m, RewardPoints = 12 },
                new Order { OrderId = 15, OrderCode = "HD20261007-002", CreatedDate = new DateTime(2026, 10, 7, 18, 45, 0), CustomerId = 15, CashierUsername = "cashier01", TotalItems = 4, FinalAmount = 40000m, RewardPoints = 4 }
            );

            // 6. DATA SEEDING: BẢNG ORDERDETAILS (18 Dòng chi tiết khớp với các hóa đơn và sản phẩm)
            modelBuilder.Entity<OrderDetail>().HasData(
                // Đơn 1: 1 Coca (10k), 1 Snack O'Star (12.5k), 1 Sữa tươi 1L (34.5k) => 57k
                new OrderDetail { OrderDetailId = 1, OrderId = 1, ProductId = 5, Quantity = 1, UnitPrice = 10000m, LineTotal = 10000m },
                new OrderDetail { OrderDetailId = 2, OrderId = 1, ProductId = 1, Quantity = 1, UnitPrice = 12500m, LineTotal = 12500m },
                new OrderDetail { OrderDetailId = 3, OrderId = 1, ProductId = 9, Quantity = 1, UnitPrice = 34500m, LineTotal = 34500m },

                // Đơn 2: 1 Bánh Danisa (135k), 1 Coca (10k) => 145k
                new OrderDetail { OrderDetailId = 4, OrderId = 2, ProductId = 2, Quantity = 1, UnitPrice = 135000m, LineTotal = 135000m },
                new OrderDetail { OrderDetailId = 5, OrderId = 2, ProductId = 5, Quantity = 1, UnitPrice = 10000m, LineTotal = 10000m },

                // Đơn 3: 10 Mì Hảo Hảo (4.5k) => 45k
                new OrderDetail { OrderDetailId = 6, OrderId = 3, ProductId = 13, Quantity = 10, UnitPrice = 4500m, LineTotal = 45000m },

                // Đơn 4: 4 Sữa tươi 1L (34.5k) => 138k
                new OrderDetail { OrderDetailId = 7, OrderId = 4, ProductId = 9, Quantity = 4, UnitPrice = 34500m, LineTotal = 138000m },

                // Đơn 5: 1 Sữa tươi 1L (34.5k), 1 Phô mai con bò cười (34.5k ~ 39k - khuyến mãi) => 69k
                new OrderDetail { OrderDetailId = 8, OrderId = 5, ProductId = 9, Quantity = 1, UnitPrice = 34500m, LineTotal = 34500m },
                new OrderDetail { OrderDetailId = 9, OrderId = 5, ProductId = 9, Quantity = 1, UnitPrice = 34500m, LineTotal = 34500m },

                // Đơn 6: 1 Bánh Chocopie (54k - giảm 500đ) => 53.5k
                new OrderDetail { OrderDetailId = 10, OrderId = 6, ProductId = 4, Quantity = 1, UnitPrice = 53500m, LineTotal = 53500m },

                // Đơn 7: 5 Nước khoáng Lavie (6k) => 30k
                new OrderDetail { OrderDetailId = 11, OrderId = 7, ProductId = 6, Quantity = 5, UnitPrice = 6000m, LineTotal = 30000m },

                // Đơn 8: 1 Dầu ăn Simply 2L (125k), 1 Nước mắm Nam Ngư (30k) => 155k
                new OrderDetail { OrderDetailId = 12, OrderId = 8, ProductId = 16, Quantity = 1, UnitPrice = 125000m, LineTotal = 125000m },
                new OrderDetail { OrderDetailId = 13, OrderId = 8, ProductId = 17, Quantity = 1, UnitPrice = 30000m, LineTotal = 30000m },

                // Đơn 9: 2 Kẹo Chupa Chups (15k), 1 Trà Xanh Không Độ (11k)
                new OrderDetail { OrderDetailId = 14, OrderId = 9, ProductId = 3, Quantity = 2, UnitPrice = 15000m, LineTotal = 30000m },
                new OrderDetail { OrderDetailId = 15, OrderId = 9, ProductId = 7, Quantity = 1, UnitPrice = 11000m, LineTotal = 11000m },

                // Đơn 10: 2 Bánh Chocopie (54k), 2 Hạt nêm Knorr (30k)
                new OrderDetail { OrderDetailId = 16, OrderId = 10, ProductId = 4, Quantity = 2, UnitPrice = 54000m, LineTotal = 108000m },
                new OrderDetail { OrderDetailId = 17, OrderId = 10, ProductId = 18, Quantity = 2, UnitPrice = 30000m, LineTotal = 60000m },

                // Đơn 14: 1 Dầu ăn Simply (125k)
                new OrderDetail { OrderDetailId = 18, OrderId = 14, ProductId = 16, Quantity = 1, UnitPrice = 125000m, LineTotal = 125000m }
            );
        }
    }
}