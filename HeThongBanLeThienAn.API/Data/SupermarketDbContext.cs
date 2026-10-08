using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(DbContextOptions<SupermarketDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Data Seeding cho Category
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Bánh quy bơ",
                    Description = "Sản phẩm bánh kẹo",
                    StockQuantity = 120
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Nước suối 500ml",
                    Description = "Nước uống đóng chai",
                    StockQuantity = 240
                },
                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Sữa tươi hộp 1L",
                    Description = "Sản phẩm từ sữa",
                    StockQuantity = 85
                },
                new Category
                {
                    CategoryId = 4,
                    CategoryName = "Mì ăn liền",
                    Description = "Thực phẩm đóng gói",
                    StockQuantity = 160
                },
                new Category
                {
                    CategoryId = 5,
                    CategoryName = "Dầu ăn thực vật",
                    Description = "Gia vị và dầu ăn",
                    StockQuantity = 60
                }
            );

            // Data Seeding cho Customer
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    CustomerName = "Nguyễn Văn A",
                    PhoneNumber = "0901234567",
                    Address = "123 Lê Lợi, Q.1, TP.HCM",
                    RewardPoints = 500,
                    MembershipRank = "Vàng"
                },
                new Customer
                {
                    CustomerId = 2,
                    CustomerName = "Trần Thị B",
                    PhoneNumber = "0987654321",
                    Address = "456 Nguyễn Huệ, Q.1, TP.HCM",
                    RewardPoints = 250,
                    MembershipRank = "Bạc"
                },
                new Customer
                {
                    CustomerId = 3,
                    CustomerName = "Lê Văn C",
                    PhoneNumber = "0911223344",
                    Address = "789 Cách Mạng Tháng 8, Q.3, TP.HCM",
                    RewardPoints = 50,
                    MembershipRank = "Chuẩn"
                }
            );
        }
    }
}
