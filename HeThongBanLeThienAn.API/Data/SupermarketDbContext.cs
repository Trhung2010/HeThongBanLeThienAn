using System;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    /// <summary>
    /// Ngữ cảnh cơ sở dữ liệu đại diện cho phiên làm việc với SQL Server
    /// Quản lý 6 thực thể và cấu hình Data Seeding đầy đủ cho hệ thống Siêu thị Mini
    /// </summary>
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(DbContextOptions options) :
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
                new User { UserId = 1, Username = "admin", PasswordHash = "admin@123", FullName = "Nhân viên mẫu 01", Role = "Admin", IsActive = true },
                new User { UserId = 2, Username = "manager01", PasswordHash = "manager@123", FullName = "Nhân viên mẫu 02", Role = "Admin", IsActive = true },
                new User { UserId = 3, Username = "cashier01", PasswordHash = "cashier@123", FullName = "Nhân viên mẫu 03", Role = "Cashier", IsActive = true },
                new User { UserId = 4, Username = "cashier02", PasswordHash = "cashier@123", FullName = "Nhân viên mẫu 04", Role = "Cashier", IsActive = true },
                new User { UserId = 5, Username = "cashier03", PasswordHash = "cashier@123", FullName = "Nhân viên mẫu 05", Role = "Cashier", IsActive = true },
                new User { UserId = 6, Username = "cashier04", PasswordHash = "cashier@123", FullName = "Nhân viên mẫu 06", Role = "Cashier", IsActive = true },
                new User { UserId = 7, Username = "cashier05", PasswordHash = "cashier@123", FullName = "Nhân viên mẫu 07", Role = "Cashier", IsActive = true },
                new User { UserId = 8, Username = "cashier06", PasswordHash = "cashier@123", FullName = "Nhân viên mẫu 08", Role = "Cashier", IsActive = true },
                new User { UserId = 9, Username = "cashier07", PasswordHash = "cashier@123", FullName = "Nhân viên mẫu 09", Role = "Cashier", IsActive = true },
                new User { UserId = 10, Username = "warehouse01", PasswordHash = "wh@123", FullName = "Nhân viên mẫu 10", Role = "Warehouse", IsActive = true },
                new User { UserId = 11, Username = "warehouse02", PasswordHash = "wh@123", FullName = "Nhân viên mẫu 11", Role = "Warehouse", IsActive = true },
                new User { UserId = 12, Username = "warehouse03", PasswordHash = "wh@123", FullName = "Nhân viên mẫu 12", Role = "Warehouse", IsActive = true },
                new User { UserId = 13, Username = "cashier08", PasswordHash = "cashier@123", FullName = "Nhân viên mẫu 13", Role = "Cashier", IsActive = true },
                new User { UserId = 14, Username = "cashier09", PasswordHash = "cashier@123", FullName = "Nhân viên mẫu 14", Role = "Cashier", IsActive = true },
                new User { UserId = 15, Username = "staff_lock", PasswordHash = "lock@123", FullName = "Nhân viên mẫu 15", Role = "Cashier", IsActive = false }
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
                new Product { ProductId = 1, DataSource = "Kho tổng Thiên Ân", Barcode = "893456789001", ProductName = "Snack Khoai Tây O'Star Gói 60g", Price = 12500m, StockQuantity = 150, CategoryId = 1 },
                new Product { ProductId = 2, DataSource = "Kho tổng Thiên Ân", Barcode = "893456789002", ProductName = "Bánh Quy Bơ Danisa Hộp Thiếc 454g", Price = 135000m, StockQuantity = 40, CategoryId = 1 },
                new Product { ProductId = 3, DataSource = "Kho tổng Thiên Ân", Barcode = "893456789003", ProductName = "Kẹo Dẻo Chupa Chups Hương Trái Cây 90g", Price = 15000m, StockQuantity = 120, CategoryId = 1 },
                new Product { ProductId = 4, DataSource = "Kho tổng Thiên Ân", Barcode = "893456789004", ProductName = "Bánh Chocopie Orion Hộp 12 Cái", Price = 54000m, StockQuantity = 60, CategoryId = 1 },
                new Product { ProductId = 5, DataSource = "Kho tổng Thiên Ân", Barcode = "893456789005", ProductName = "Nước Ngọt Coca Cola Lon 320ml", Price = 10000m, StockQuantity = 320, CategoryId = 2 },
                new Product { ProductId = 6, DataSource = "Kho tổng Thiên Ân", Barcode = "893456789006", ProductName = "Nước Khoáng Thiên Nhiên Lavie Chai 500ml", Price = 6000m, StockQuantity = 200, CategoryId = 2 },
                new Product { ProductId = 7, DataSource = "Kho tổng Thiên Ân", Barcode = "893456789007", ProductName = "Trà Xanh Không Độ Chai 455ml", Price = 11000m, StockQuantity = 180, CategoryId = 2 },
                new Product { ProductId = 8, DataSource = "Kho tổng Thiên Ân", Barcode = "893456789008", ProductName = "Nước Tăng Lực Red Bull Lon 250ml", Price = 14500m, StockQuantity = 150, CategoryId = 2 },
                new Product { ProductId = 9, DataSource = "Kho tổng Thiên Ân", Barcode = "893456789009", ProductName = "Sữa Tươi Tiệt Trùng Vinamilk Có Đường 1L", Price = 34500m, StockQuantity = 85, CategoryId = 3 },
                new Product { ProductId = 10, DataSource = "Kho tổng Thiên Ân", Barcode = "893456789010", ProductName = "Phô Mai Con Bò Cười Hộp 8 Miếng", Price = 39000m, StockQuantity = 60, CategoryId = 3 },
                new Product { ProductId = 11, DataSource = "Kho tổng Thiên Ân", Barcode = "893456789011", ProductName = "Sữa Chua Ăn Vinamilk Có Đường Lốc 4 Hộp", Price = 28000m, StockQuantity = 95, CategoryId = 3 },
                new Product { ProductId = 12, DataSource = "Kho tổng Thiên Ân", Barcode = "893456789012", ProductName = "Sữa Đặc Có Đường Ông Thọ Lon 380g", Price = 26000m, StockQuantity = 110, CategoryId = 3 },
                new Product { ProductId = 13, DataSource = "Kho tổng Thiên Ân", Barcode = "893456789013", ProductName = "Mì Tôm Hảo Hảo Chua Cay Gói 75g", Price = 4500m, StockQuantity = 500, CategoryId = 4 },
                new Product { ProductId = 14, DataSource = "Kho tổng Thiên Ân", Barcode = "893456789014", ProductName = "Phở Bò Đệ Nhất Gói 68g", Price = 8500m, StockQuantity = 140, CategoryId = 4 },
                new Product { ProductId = 15, DataSource = "Kho tổng Thiên Ân", Barcode = "893456789015", ProductName = "Mì Trộn Tương Đen Koreno Gói 115g", Price = 14000m, StockQuantity = 80, CategoryId = 4 },
                new Product { ProductId = 16, DataSource = "Kho tổng Thiên Ân", Barcode = "893456789016", ProductName = "Dầu Ăn Simply Nguyên Chất Can 2L", Price = 125000m, StockQuantity = 45, CategoryId = 5 },
                new Product { ProductId = 17, DataSource = "Kho tổng Thiên Ân", Barcode = "893456789017", ProductName = "Nước Mắm Nam Ngư Đệ Nhị Chai 900ml", Price = 30000m, StockQuantity = 90, CategoryId = 5 },
                new Product { ProductId = 18, DataSource = "Kho tổng Thiên Ân", Barcode = "893456789018", ProductName = "Hạt Nêm Knorr Từ Thịt Thăn Gói 400g", Price = 36000m, StockQuantity = 75, CategoryId = 5 },
                new Product { ProductId = 19, DataSource = "Kho tổng Thiên Ân", Barcode = "893456789019", ProductName = "Cá Nục Sốt Cà Ba Cô Gái Hộp 155g", Price = 19000m, StockQuantity = 130, CategoryId = 6 },
                new Product { ProductId = 20, DataSource = "Kho tổng Thiên Ân", Barcode = "893456789020", ProductName = "Nước Rửa Chén Sunlight Chanh Chai 750g", Price = 29000m, StockQuantity = 80, CategoryId = 7 }
            );

            // 4. DATA SEEDING: BẢNG CUSTOMERS — để trống, nhập dữ liệu thực qua WinForm/API

            // 5. DATA SEEDING: BẢNG ORDERS (15 Hóa đơn bán lẻ phát sinh tại quầy)
            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, OrderCode = "HD20261001-001", CreatedDate = new DateTime(2026, 10, 1, 8, 30, 0), CustomerId = null, CashierUsername = "cashier01", TotalItems = 3, FinalAmount = 57000m, RewardPoints = 0 },
                new Order { OrderId = 2, OrderCode = "HD20261001-002", CreatedDate = new DateTime(2026, 10, 1, 9, 15, 0), CustomerId = null, CashierUsername = "cashier01", TotalItems = 2, FinalAmount = 145000m, RewardPoints = 0 },
                new Order { OrderId = 3, OrderCode = "HD20261001-003", CreatedDate = new DateTime(2026, 10, 1, 10, 0, 0), CustomerId = null, CashierUsername = "cashier02", TotalItems = 10, FinalAmount = 45000m, RewardPoints = 0 },
                new Order { OrderId = 4, OrderCode = "HD20261002-001", CreatedDate = new DateTime(2026, 10, 2, 8, 45, 0), CustomerId = null, CashierUsername = "cashier02", TotalItems = 4, FinalAmount = 138000m, RewardPoints = 0 },
                new Order { OrderId = 5, OrderCode = "HD20261002-002", CreatedDate = new DateTime(2026, 10, 2, 11, 20, 0), CustomerId = null, CashierUsername = "cashier03", TotalItems = 2, FinalAmount = 69000m, RewardPoints = 0 },
                new Order { OrderId = 6, OrderCode = "HD20261003-001", CreatedDate = new DateTime(2026, 10, 3, 14, 10, 0), CustomerId = null, CashierUsername = "cashier03", TotalItems = 1, FinalAmount = 53500m, RewardPoints = 0 },
                new Order { OrderId = 7, OrderCode = "HD20261003-002", CreatedDate = new DateTime(2026, 10, 3, 16, 40, 0), CustomerId = null, CashierUsername = "cashier04", TotalItems = 5, FinalAmount = 30000m, RewardPoints = 0 },
                new Order { OrderId = 8, OrderCode = "HD20261004-001", CreatedDate = new DateTime(2026, 10, 4, 9, 05, 0), CustomerId = null, CashierUsername = "cashier04", TotalItems = 2, FinalAmount = 155000m, RewardPoints = 0 },
                new Order { OrderId = 9, OrderCode = "HD20261004-002", CreatedDate = new DateTime(2026, 10, 4, 13, 30, 0), CustomerId = null, CashierUsername = "cashier05", TotalItems = 3, FinalAmount = 41000m, RewardPoints = 0 },
                new Order { OrderId = 10, OrderCode = "HD20261005-001", CreatedDate = new DateTime(2026, 10, 5, 10, 50, 0), CustomerId = null, CashierUsername = "cashier05", TotalItems = 4, FinalAmount = 168000m, RewardPoints = 0 },
                new Order { OrderId = 11, OrderCode = "HD20261005-002", CreatedDate = new DateTime(2026, 10, 5, 17, 15, 0), CustomerId = null, CashierUsername = "cashier06", TotalItems = 8, FinalAmount = 36000m, RewardPoints = 0 },
                new Order { OrderId = 12, OrderCode = "HD20261006-001", CreatedDate = new DateTime(2026, 10, 6, 8, 20, 0), CustomerId = null, CashierUsername = "cashier06", TotalItems = 2, FinalAmount = 108000m, RewardPoints = 0 },
                new Order { OrderId = 13, OrderCode = "HD20261006-002", CreatedDate = new DateTime(2026, 10, 6, 15, 00, 0), CustomerId = null, CashierUsername = "cashier07", TotalItems = 3, FinalAmount = 78000m, RewardPoints = 0 },
                new Order { OrderId = 14, OrderCode = "HD20261007-001", CreatedDate = new DateTime(2026, 10, 7, 12, 10, 0), CustomerId = null, CashierUsername = "cashier07", TotalItems = 1, FinalAmount = 125000m, RewardPoints = 0 },
                new Order { OrderId = 15, OrderCode = "HD20261007-002", CreatedDate = new DateTime(2026, 10, 7, 18, 45, 0), CustomerId = null, CashierUsername = "cashier01", TotalItems = 4, FinalAmount = 40000m, RewardPoints = 0 }
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
                new OrderDetail { OrderDetailId = 18, OrderId = 14, ProductId = 16, Quantity = 1, UnitPrice = 125000m, LineTotal = 125000m },
                new OrderDetail { OrderDetailId = 19, OrderId = 11, ProductId = 13, Quantity = 8, UnitPrice = 4500m, LineTotal = 36000m },
                new OrderDetail { OrderDetailId = 20, OrderId = 12, ProductId = 4, Quantity = 2, UnitPrice = 54000m, LineTotal = 108000m },
                new OrderDetail { OrderDetailId = 21, OrderId = 13, ProductId = 12, Quantity = 3, UnitPrice = 26000m, LineTotal = 78000m },
                new OrderDetail { OrderDetailId = 22, OrderId = 15, ProductId = 5, Quantity = 4, UnitPrice = 10000m, LineTotal = 40000m }
            );

            // Reference catalog; mã số nội bộ, không phải barcode đã xác minh từ nhà sản xuất.
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 21, Barcode = "893456789021", ProductName = "Bánh quy sữa gói 100g", Price = 18000m, StockQuantity = 0, CategoryId = 1, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 22, Barcode = "893456789022", ProductName = "Bánh cracker gói 150g", Price = 22000m, StockQuantity = 0, CategoryId = 1, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 23, Barcode = "893456789023", ProductName = "Bánh gạo gói 150g", Price = 25000m, StockQuantity = 0, CategoryId = 1, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 24, Barcode = "893456789024", ProductName = "Kẹo bạc hà gói 100g", Price = 15000m, StockQuantity = 0, CategoryId = 1, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 25, Barcode = "893456789025", ProductName = "Kẹo sữa gói 100g", Price = 18000m, StockQuantity = 0, CategoryId = 1, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 26, Barcode = "893456789026", ProductName = "Hạt điều rang hộp 200g", Price = 65000m, StockQuantity = 0, CategoryId = 1, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 27, Barcode = "893456789027", ProductName = "Đậu phộng rang gói 100g", Price = 12000m, StockQuantity = 0, CategoryId = 1, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 28, Barcode = "893456789028", ProductName = "Rong biển ăn liền gói 20g", Price = 15000m, StockQuantity = 0, CategoryId = 1, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 29, Barcode = "893456789029", ProductName = "Nước tinh khiết chai 1.5L", Price = 10000m, StockQuantity = 0, CategoryId = 2, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 30, Barcode = "893456789030", ProductName = "Nước khoáng chai 350ml", Price = 5000m, StockQuantity = 0, CategoryId = 2, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 31, Barcode = "893456789031", ProductName = "Trà đào chai 450ml", Price = 12000m, StockQuantity = 0, CategoryId = 2, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 32, Barcode = "893456789032", ProductName = "Trà ô long chai 450ml", Price = 12000m, StockQuantity = 0, CategoryId = 2, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 33, Barcode = "893456789033", ProductName = "Nước cam chai 350ml", Price = 15000m, StockQuantity = 0, CategoryId = 2, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 34, Barcode = "893456789034", ProductName = "Nước chanh chai 350ml", Price = 10000m, StockQuantity = 0, CategoryId = 2, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 35, Barcode = "893456789035", ProductName = "Nước dừa hộp 1L", Price = 35000m, StockQuantity = 0, CategoryId = 2, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 36, Barcode = "893456789036", ProductName = "Nước yến lon 190ml", Price = 10000m, StockQuantity = 0, CategoryId = 2, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 37, Barcode = "893456789037", ProductName = "Sữa tươi không đường hộp 180ml", Price = 8000m, StockQuantity = 0, CategoryId = 3, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 38, Barcode = "893456789038", ProductName = "Sữa tươi có đường hộp 180ml", Price = 8000m, StockQuantity = 0, CategoryId = 3, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 39, Barcode = "893456789039", ProductName = "Sữa đậu nành hộp 1L", Price = 28000m, StockQuantity = 0, CategoryId = 3, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 40, Barcode = "893456789040", ProductName = "Sữa chua uống chai 180ml", Price = 9000m, StockQuantity = 0, CategoryId = 3, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 41, Barcode = "893456789041", ProductName = "Sữa chua không đường hộp 100g", Price = 6500m, StockQuantity = 0, CategoryId = 3, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 42, Barcode = "893456789042", ProductName = "Sữa bột hộp 400g", Price = 180000m, StockQuantity = 0, CategoryId = 3, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 43, Barcode = "893456789043", ProductName = "Bơ lạt gói 200g", Price = 60000m, StockQuantity = 0, CategoryId = 3, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 44, Barcode = "893456789044", ProductName = "Phô mai lát gói 200g", Price = 55000m, StockQuantity = 0, CategoryId = 3, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 45, Barcode = "893456789045", ProductName = "Mì bò gói 75g", Price = 4500m, StockQuantity = 0, CategoryId = 4, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 46, Barcode = "893456789046", ProductName = "Mì chay gói 75g", Price = 4500m, StockQuantity = 0, CategoryId = 4, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 47, Barcode = "893456789047", ProductName = "Cháo thịt gói 50g", Price = 7000m, StockQuantity = 0, CategoryId = 4, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 48, Barcode = "893456789048", ProductName = "Miến gà gói 58g", Price = 10000m, StockQuantity = 0, CategoryId = 4, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 49, Barcode = "893456789049", ProductName = "Bún bò gói 75g", Price = 12000m, StockQuantity = 0, CategoryId = 4, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 50, Barcode = "893456789050", ProductName = "Hủ tiếu gói 65g", Price = 8500m, StockQuantity = 0, CategoryId = 4, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 51, Barcode = "893456789051", ProductName = "Phở gà gói 65g", Price = 8500m, StockQuantity = 0, CategoryId = 4, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 52, Barcode = "893456789052", ProductName = "Nui khô gói 400g", Price = 22000m, StockQuantity = 0, CategoryId = 4, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 53, Barcode = "893456789053", ProductName = "Đường cát trắng túi 1kg", Price = 26000m, StockQuantity = 0, CategoryId = 5, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 54, Barcode = "893456789054", ProductName = "Muối iốt gói 500g", Price = 8000m, StockQuantity = 0, CategoryId = 5, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 55, Barcode = "893456789055", ProductName = "Bột ngọt gói 400g", Price = 32000m, StockQuantity = 0, CategoryId = 5, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 56, Barcode = "893456789056", ProductName = "Tiêu xay lọ 50g", Price = 18000m, StockQuantity = 0, CategoryId = 5, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 57, Barcode = "893456789057", ProductName = "Nước tương chai 500ml", Price = 22000m, StockQuantity = 0, CategoryId = 5, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 58, Barcode = "893456789058", ProductName = "Tương ớt chai 250g", Price = 15000m, StockQuantity = 0, CategoryId = 5, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 59, Barcode = "893456789059", ProductName = "Giấm gạo chai 500ml", Price = 14000m, StockQuantity = 0, CategoryId = 5, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 60, Barcode = "893456789060", ProductName = "Dầu hào chai 350g", Price = 28000m, StockQuantity = 0, CategoryId = 5, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 61, Barcode = "893456789061", ProductName = "Cá ngừ ngâm dầu hộp 185g", Price = 35000m, StockQuantity = 0, CategoryId = 6, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 62, Barcode = "893456789062", ProductName = "Pate gan hộp 150g", Price = 25000m, StockQuantity = 0, CategoryId = 6, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 63, Barcode = "893456789063", ProductName = "Thịt heo hộp 170g", Price = 40000m, StockQuantity = 0, CategoryId = 6, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 64, Barcode = "893456789064", ProductName = "Bắp ngọt hộp 340g", Price = 26000m, StockQuantity = 0, CategoryId = 6, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 65, Barcode = "893456789065", ProductName = "Đậu Hà Lan hộp 400g", Price = 28000m, StockQuantity = 0, CategoryId = 6, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 66, Barcode = "893456789066", ProductName = "Xúc xích gói 200g", Price = 32000m, StockQuantity = 0, CategoryId = 6, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 67, Barcode = "893456789067", ProductName = "Chà bông heo hộp 100g", Price = 55000m, StockQuantity = 0, CategoryId = 6, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 68, Barcode = "893456789068", ProductName = "Dưa leo muối lọ 500g", Price = 35000m, StockQuantity = 0, CategoryId = 6, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 69, Barcode = "893456789069", ProductName = "Nước giặt túi 2L", Price = 95000m, StockQuantity = 0, CategoryId = 7, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 70, Barcode = "893456789070", ProductName = "Nước xả vải túi 1.6L", Price = 65000m, StockQuantity = 0, CategoryId = 7, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 71, Barcode = "893456789071", ProductName = "Bột giặt túi 800g", Price = 35000m, StockQuantity = 0, CategoryId = 7, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 72, Barcode = "893456789072", ProductName = "Nước lau sàn chai 1L", Price = 32000m, StockQuantity = 0, CategoryId = 7, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 73, Barcode = "893456789073", ProductName = "Nước tẩy nhà vệ sinh chai 900ml", Price = 38000m, StockQuantity = 0, CategoryId = 7, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 74, Barcode = "893456789074", ProductName = "Nước lau kính chai 500ml", Price = 25000m, StockQuantity = 0, CategoryId = 7, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 75, Barcode = "893456789075", ProductName = "Nước rửa tay chai 250ml", Price = 28000m, StockQuantity = 0, CategoryId = 7, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 76, Barcode = "893456789076", ProductName = "Miếng rửa chén bộ 3 miếng", Price = 12000m, StockQuantity = 0, CategoryId = 7, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 77, Barcode = "893456789077", ProductName = "Dầu gội chai 340ml", Price = 75000m, StockQuantity = 0, CategoryId = 8, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 78, Barcode = "893456789078", ProductName = "Sữa tắm chai 500ml", Price = 85000m, StockQuantity = 0, CategoryId = 8, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 79, Barcode = "893456789079", ProductName = "Kem đánh răng tuýp 180g", Price = 35000m, StockQuantity = 0, CategoryId = 8, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 80, Barcode = "893456789080", ProductName = "Bàn chải đánh răng chiếc", Price = 18000m, StockQuantity = 0, CategoryId = 8, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 81, Barcode = "893456789081", ProductName = "Xà phòng bánh 90g", Price = 12000m, StockQuantity = 0, CategoryId = 8, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 82, Barcode = "893456789082", ProductName = "Khăn ướt gói 80 tờ", Price = 25000m, StockQuantity = 0, CategoryId = 8, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 83, Barcode = "893456789083", ProductName = "Băng vệ sinh gói 8 miếng", Price = 28000m, StockQuantity = 0, CategoryId = 8, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 84, Barcode = "893456789084", ProductName = "Tăm bông hộp 200 que", Price = 15000m, StockQuantity = 0, CategoryId = 8, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 85, Barcode = "893456789085", ProductName = "Khăn giấy hộp 180 tờ", Price = 22000m, StockQuantity = 0, CategoryId = 9, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 86, Barcode = "893456789086", ProductName = "Giấy vệ sinh lốc 10 cuộn", Price = 55000m, StockQuantity = 0, CategoryId = 9, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 87, Barcode = "893456789087", ProductName = "Túi rác cuộn 500g", Price = 25000m, StockQuantity = 0, CategoryId = 9, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 88, Barcode = "893456789088", ProductName = "Màng bọc thực phẩm cuộn 30m", Price = 28000m, StockQuantity = 0, CategoryId = 9, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 89, Barcode = "893456789089", ProductName = "Giấy bạc cuộn 5m", Price = 25000m, StockQuantity = 0, CategoryId = 9, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 90, Barcode = "893456789090", ProductName = "Pin AA vỉ 2 viên", Price = 15000m, StockQuantity = 0, CategoryId = 9, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 91, Barcode = "893456789091", ProductName = "Pin AAA vỉ 2 viên", Price = 15000m, StockQuantity = 0, CategoryId = 9, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 92, Barcode = "893456789092", ProductName = "Găng tay thực phẩm hộp 100 chiếc", Price = 20000m, StockQuantity = 0, CategoryId = 9, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 93, Barcode = "893456789093", ProductName = "Cà phê hòa tan hộp 20 gói", Price = 55000m, StockQuantity = 0, CategoryId = 13, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 94, Barcode = "893456789094", ProductName = "Cà phê rang xay gói 250g", Price = 65000m, StockQuantity = 0, CategoryId = 13, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 95, Barcode = "893456789095", ProductName = "Yến mạch túi 500g", Price = 45000m, StockQuantity = 0, CategoryId = 13, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 96, Barcode = "893456789096", ProductName = "Ngũ cốc dinh dưỡng túi 400g", Price = 48000m, StockQuantity = 0, CategoryId = 13, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 97, Barcode = "893456789097", ProductName = "Trà túi lọc hộp 25 túi", Price = 35000m, StockQuantity = 0, CategoryId = 13, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 98, Barcode = "893456789098", ProductName = "Bột cacao hộp 200g", Price = 55000m, StockQuantity = 0, CategoryId = 13, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 99, Barcode = "893456789099", ProductName = "Bột đậu xanh túi 500g", Price = 38000m, StockQuantity = 0, CategoryId = 13, DataSource = "Kho tổng Thiên Ân" },
                new Product { ProductId = 100, Barcode = "893456789100", ProductName = "Gạo trắng túi 5kg", Price = 95000m, StockQuantity = 0, CategoryId = 13, DataSource = "Kho tổng Thiên Ân" }
            );
        }
    }

    // Context SQL Server dùng cho runtime và migration của ứng dụng.
    public sealed class SqlServerSupermarketDbContext : SupermarketDbContext
    {
        public SqlServerSupermarketDbContext(DbContextOptions<SqlServerSupermarketDbContext> options)
            : base(options) { }
    }
}
