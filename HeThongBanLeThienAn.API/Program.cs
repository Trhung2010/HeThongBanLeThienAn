using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình Entity Framework Core DbContext với SQLite
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SupermarketDbContext>(options =>
{
    if (!string.IsNullOrEmpty(connectionString))
    {
        // ĐÃ SỬA: Dùng UseSqlite thay cho UseSqlServer
        options.UseSqlite(connectionString);
    }
    else
    {
        options.UseInMemoryDatabase("MiniSupermarketDb");
    }
});

// Cấu hình dịch vụ xác thực JWT Bearer
var jwtSecret = builder.Configuration["JwtSettings:Secret"] ??
"SupermarketSecretKeyDoAnMonHoc2026SecureString!!";

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Cấu hình dịch vụ và middleware khác
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Đảm bảo CSDL được khởi tạo và nạp dữ liệu mẫu khi ứng dụng khởi chạy
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SupermarketDbContext>();
    try
    {
        dbContext.Database.EnsureCreated();

        if (dbContext.Categories.Count() < 15)
        {
            dbContext.Categories.RemoveRange(dbContext.Categories);
            dbContext.SaveChanges();

            dbContext.Categories.AddRange(
                new Category { CategoryId = 1, CategoryName = "Bánh kẹo & Snack ăn vặt", Description = "Các loại bánh quy, bánh bông lan, kẹo và snack", StockQuantity = 350 },
                new Category { CategoryId = 2, CategoryName = "Nước giải khát & Nước suối", Description = "Nước ngọt có gas, nước khoáng, nước tăng lực và trà đóng chai", StockQuantity = 480 },
                new Category { CategoryId = 3, CategoryName = "Sữa & Sản phẩm từ sữa", Description = "Sữa tươi tiệt trùng, sữa chua ăn, sữa chua uống và phô mai", StockQuantity = 210 },
                new Category { CategoryId = 4, CategoryName = "Mì ăn liền & Thực phẩm đóng gói", Description = "Mì gói, bún, miến, cháo ăn liền và xúc xích tiệt trùng", StockQuantity = 520 },
                new Category { CategoryId = 5, CategoryName = "Gia vị & Dầu ăn nấu nướng", Description = "Dầu ăn thực vật, nước mắm, nước tương, muối tinh và bột ngọt", StockQuantity = 180 },
                new Category { CategoryId = 6, CategoryName = "Gạo, Đậu & Nông sản khô", Description = "Gạo thơm các loại, nếp, đậu xanh, nấm mèo và mộc nhĩ khô", StockQuantity = 150 },
                new Category { CategoryId = 7, CategoryName = "Cà phê, Trà & Đồ uống hòa tan", Description = "Cà phê hòa tan 3in1, cà phê đen, trà túi lọc và bột cacao", StockQuantity = 140 },
                new Category { CategoryId = 8, CategoryName = "Hóa phẩm vệ sinh nhà cửa", Description = "Nước rửa chén, nước lau sàn, nước tẩy bồn cầu và xịt kính", StockQuantity = 190 },
                new Category { CategoryId = 9, CategoryName = "Chăm sóc cá nhân & Cơ thể", Description = "Dầu gội đầu, sữa tắm, kem đánh răng và xà phòng rửa tay", StockQuantity = 220 },
                new Category { CategoryId = 10, CategoryName = "Giặt xả & Làm sạch quần áo", Description = "Bột giặt, nước giặt xả đậm đặc và nước ngâm thơm quần áo", StockQuantity = 160 },
                new Category { CategoryId = 11, CategoryName = "Thực phẩm đông mát & Bảo quản", Description = "Chả cá viên, xúc xích tươi, tôm viên và há cảo đông lạnh", StockQuantity = 110 },
                new Category { CategoryId = 12, CategoryName = "Đồ hộp & Thực phẩm chế biến sẵn", Description = "Cá hộp sốt cà, thịt hộp pate, ngô ngọt và nấm đóng hộp", StockQuantity = 130 },
                new Category { CategoryId = 13, CategoryName = "Khăn giấy & Màng bọc thực phẩm", Description = "Khăn giấy lụa, giấy vệ sinh cuộn, màng bọc thức ăn và túi rác", StockQuantity = 260 },
                new Category { CategoryId = 14, CategoryName = "Đồ dùng nhà bếp & Gia dụng tiện ích", Description = "Miếng rửa bát, bao tay cao su, đũa gỗ, kẹp đồ và bật lửa", StockQuantity = 175 },
                new Category { CategoryId = 15, CategoryName = "Văn phòng phẩm & Đồ chơi trẻ em", Description = "Bút bi, tập vở học sinh, băng keo và đồ chơi mô hình nhỏ", StockQuantity = 95 }
            );
            dbContext.SaveChanges();
        }

        if (dbContext.Customers.Count() < 15)
        {
            dbContext.Customers.RemoveRange(dbContext.Customers);
            dbContext.SaveChanges();

            dbContext.Customers.AddRange(
                new Customer { CustomerId = 1, CustomerName = "Nguyễn Văn A", PhoneNumber = "0901234567", Address = "123 Lê Lợi, Phường Bến Nghé, Quận 1, TP.HCM", RewardPoints = 520, MembershipRank = "Vàng" },
                new Customer { CustomerId = 2, CustomerName = "Trần Thị Bích Ngọc", PhoneNumber = "0987654321", Address = "456 Nguyễn Huệ, Phường Bến Nghé, Quận 1, TP.HCM", RewardPoints = 280, MembershipRank = "Bạc" },
                new Customer { CustomerId = 3, CustomerName = "Lê Văn Cường", PhoneNumber = "0911223344", Address = "789 Cách Mạng Tháng 8, Phường 6, Quận 3, TP.HCM", RewardPoints = 60, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 4, CustomerName = "Phạm Minh Tuấn", PhoneNumber = "0933456789", Address = "12 Hoàng Hoa Thám, Phường 13, Quận Tân Bình, TP.HCM", RewardPoints = 850, MembershipRank = "Vàng" },
                new Customer { CustomerId = 5, CustomerName = "Đỗ Thị Thanh Thảo", PhoneNumber = "0978112233", Address = "88 Quang Trung, Phường 10, Quận Gò Vấp, TP.HCM", RewardPoints = 1200, MembershipRank = "Kim Cương" },
                new Customer { CustomerId = 6, CustomerName = "Vũ Hoàng Nam", PhoneNumber = "0945667788", Address = "25 Đường số 7, Phường Linh Trung, TP. Thủ Đức", RewardPoints = 150, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 7, CustomerName = "Hoàng Bích Thủy", PhoneNumber = "0898334455", Address = "302 Hai Bà Trưng, Phường Tân Định, Quận 1, TP.HCM", RewardPoints = 410, MembershipRank = "Bạc" },
                new Customer { CustomerId = 8, CustomerName = "Ngô Quốc Bảo", PhoneNumber = "0969778899", Address = "155 Sư Vạn Hạnh, Phường 12, Quận 10, TP.HCM", RewardPoints = 630, MembershipRank = "Vàng" },
                new Customer { CustomerId = 9, CustomerName = "Bùi Tuyết Mai", PhoneNumber = "0908889900", Address = "47 Phan Xích Long, Phường 2, Quận Phú Nhuận, TP.HCM", RewardPoints = 310, MembershipRank = "Bạc" },
                new Customer { CustomerId = 10, CustomerName = "Đặng Hữu Phước", PhoneNumber = "0918776655", Address = "68 Nguyễn Thị Thập, Phường Tân Phú, Quận 7, TP.HCM", RewardPoints = 90, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 11, CustomerName = "Trương Mỹ Linh", PhoneNumber = "0938445566", Address = "204 Điện Biên Phủ, Phường 15, Quận Bình Thạnh, TP.HCM", RewardPoints = 1550, MembershipRank = "Kim Cương" },
                new Customer { CustomerId = 12, CustomerName = "Lý Gia Hưng", PhoneNumber = "0888123456", Address = "59 Hùng Vương, Phường 4, Quận 5, TP.HCM", RewardPoints = 480, MembershipRank = "Bạc" },
                new Customer { CustomerId = 13, CustomerName = "Dương Thu Trang", PhoneNumber = "0972334411", Address = "18 Lê Văn Việt, Phường Hiệp Phú, TP. Thủ Đức", RewardPoints = 720, MembershipRank = "Vàng" },
                new Customer { CustomerId = 14, CustomerName = "Hồ Văn Đức", PhoneNumber = "0903556677", Address = "102 Âu Cơ, Phường 10, Quận Tân Bình, TP.HCM", RewardPoints = 45, MembershipRank = "Chuẩn" },
                new Customer { CustomerId = 15, CustomerName = "Phan Ánh Nguyệt", PhoneNumber = "0961223388", Address = "31 Võ Văn Tần, Phường Võ Thị Sáu, Quận 3, TP.HCM", RewardPoints = 890, MembershipRank = "Vàng" }
            );
            dbContext.SaveChanges();
        }

        if (dbContext.Products.Count() < 15)
        {
            dbContext.Products.RemoveRange(dbContext.Products);
            dbContext.SaveChanges();

            dbContext.Products.AddRange(
                new Product { ProductId = 1, Barcode = "893000000001", ProductName = "Bánh quy bơ Cosy Kinh Đô 336g", Price = 42000m, StockQuantity = 45, CategoryId = 1 },
                new Product { ProductId = 2, Barcode = "893000000002", ProductName = "Nước khoáng thiên nhiên Lavie 500ml", Price = 6000m, StockQuantity = 120, CategoryId = 2 },
                new Product { ProductId = 3, Barcode = "893000000003", ProductName = "Sữa tươi tiệt trùng TH True Milk có đường 1L", Price = 38000m, StockQuantity = 35, CategoryId = 3 },
                new Product { ProductId = 4, Barcode = "893000000004", ProductName = "Mì Hảo Hảo tôm chua cay Acecook 75g", Price = 4500m, StockQuantity = 250, CategoryId = 4 },
                new Product { ProductId = 5, Barcode = "893000000005", ProductName = "Dầu đậu nành nguyên chất Simply 1L", Price = 56000m, StockQuantity = 40, CategoryId = 5 },
                new Product { ProductId = 6, Barcode = "893000000006", ProductName = "Gạo thơm ST25 Thượng hạng túi 5kg", Price = 175000m, StockQuantity = 25, CategoryId = 6 },
                new Product { ProductId = 7, Barcode = "893000000007", ProductName = "Cà phê hòa tan G7 3in1 Trung Nguyên hộp 21 gói", Price = 58000m, StockQuantity = 50, CategoryId = 7 },
                new Product { ProductId = 8, Barcode = "893000000008", ProductName = "Nước rửa chén Sunlight Chanh túi 750ml", Price = 28000m, StockQuantity = 60, CategoryId = 8 },
                new Product { ProductId = 9, Barcode = "893000000009", ProductName = "Dầu gội sạch gàu Clear Bạc Hà chai 630g", Price = 145000m, StockQuantity = 30, CategoryId = 9 },
                new Product { ProductId = 10, Barcode = "893000000010", ProductName = "Nước giặt OMO Matic cửa trên túi 3.6kg", Price = 195000m, StockQuantity = 20, CategoryId = 10 },
                new Product { ProductId = 11, Barcode = "893000000011", ProductName = "Cá viên chiên hải sản C.P gói 500g", Price = 48000m, StockQuantity = 35, CategoryId = 11 },
                new Product { ProductId = 12, Barcode = "893000000012", ProductName = "Cá nục sốt cà chua 3 Cô Gái lon 155g", Price = 16000m, StockQuantity = 80, CategoryId = 12 },
                new Product { ProductId = 13, Barcode = "893000000013", ProductName = "Khăn giấy rút cao cấp Paseo 3 lớp gói 250 tờ", Price = 22000m, StockQuantity = 90, CategoryId = 13 },
                new Product { ProductId = 14, Barcode = "893000000014", ProductName = "Miếng rửa chén bọt biển Scotch-Brite 3M vỉ 3 cái", Price = 25000m, StockQuantity = 65, CategoryId = 14 },
                new Product { ProductId = 15, Barcode = "893000000015", ProductName = "Tập học sinh 96 trang Thiên Long - Điểm 10", Price = 8500m, StockQuantity = 110, CategoryId = 15 }
            );
            dbContext.SaveChanges();
        }
    }
    catch
    {
        // Fallback for isolated build environments if DB is not running locally
    }
}

// Cấu hình middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Bắt buộc gọi UseAuthentication trước UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();