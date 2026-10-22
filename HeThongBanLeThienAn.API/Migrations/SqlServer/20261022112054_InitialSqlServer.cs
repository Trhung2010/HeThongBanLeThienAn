using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HeThongBanLeThienAn.API.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class InitialSqlServer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RewardPoints = table.Column<int>(type: "int", nullable: false),
                    MembershipRank = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataSource = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    CashierUsername = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalItems = table.Column<int>(type: "int", nullable: false),
                    FinalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RewardPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 1, "Bánh kẹo & Đồ ăn vặt", "Snack khoai tây, bánh quy bơ, kẹo trái cây" },
                    { 2, "Nước giải khát & Trà", "Nước ngọt lon, nước khoáng thiên nhiên, trà xanh đóng chai" },
                    { 3, "Sữa & Sản phẩm từ sữa", "Sữa tươi tiệt trùng, sữa đặc, sữa chua ăn, phô mai lát" },
                    { 4, "Mì gói & Thực phẩm ăn liền", "Mì ăn liền, hủ tiếu, phở khô, cháo gói dinh dưỡng" },
                    { 5, "Gia vị & Dầu ăn", "Nước mắm cá cơm, hạt nêm, dầu ăn nguyên chất, muối i-ốt" },
                    { 6, "Đồ hộp & Chế biến sẵn", "Cá hộp sốt cà, thịt heo hầm hộp, pate gan, xúc xích ăn liền" },
                    { 7, "Hóa phẩm tẩy rửa", "Nước giặt xả, nước lau sàn nhà, nước rửa chén, bột tẩy" },
                    { 8, "Chăm sóc cá nhân", "Dầu gội đầu, sữa tắm dưỡng ẩm, kem đánh răng, xà phòng" },
                    { 9, "Đồ gia dụng tiện ích", "Màng bọc thực phẩm, túi rác sinh học, khăn giấy rút, pin tiểu" },
                    { 10, "Rau củ tươi sạch", "Cà chua bi, rau cải xanh, khoai tây, cà rốt Đà Lạt" },
                    { 11, "Thịt tươi & Thủy sản", "Thịt ba chỉ heo, ức gà phi lê, cá phi lê tươi sống" },
                    { 12, "Thực phẩm đông lạnh", "Bánh bao nhân thịt, há cảo hấp, cá viên, bò viên chiên" },
                    { 13, "Cà phê & Ngũ cốc", "Cà phê hòa tan 3in1, bột yến mạch, bột ngũ cốc dinh dưỡng" },
                    { 14, "Văn phòng phẩm mini", "Bút bi bấm, vở viết học sinh, kéo cắt giấy văn phòng" },
                    { 15, "Vật tư đóng gói POS", "Bao xốp quầy thu ngân, giấy in bill nhiệt k80, tem dán nhãn" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CashierUsername", "CreatedDate", "CustomerId", "FinalAmount", "OrderCode", "RewardPoints", "TotalItems" },
                values: new object[,]
                {
                    { 1, "cashier01", new DateTime(2026, 10, 1, 8, 30, 0, 0, DateTimeKind.Unspecified), null, 57000m, "HD20261001-001", 0, 3 },
                    { 2, "cashier01", new DateTime(2026, 10, 1, 9, 15, 0, 0, DateTimeKind.Unspecified), null, 145000m, "HD20261001-002", 0, 2 },
                    { 3, "cashier02", new DateTime(2026, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), null, 45000m, "HD20261001-003", 0, 10 },
                    { 4, "cashier02", new DateTime(2026, 10, 2, 8, 45, 0, 0, DateTimeKind.Unspecified), null, 138000m, "HD20261002-001", 0, 4 },
                    { 5, "cashier03", new DateTime(2026, 10, 2, 11, 20, 0, 0, DateTimeKind.Unspecified), null, 69000m, "HD20261002-002", 0, 2 },
                    { 6, "cashier03", new DateTime(2026, 10, 3, 14, 10, 0, 0, DateTimeKind.Unspecified), null, 53500m, "HD20261003-001", 0, 1 },
                    { 7, "cashier04", new DateTime(2026, 10, 3, 16, 40, 0, 0, DateTimeKind.Unspecified), null, 30000m, "HD20261003-002", 0, 5 },
                    { 8, "cashier04", new DateTime(2026, 10, 4, 9, 5, 0, 0, DateTimeKind.Unspecified), null, 155000m, "HD20261004-001", 0, 2 },
                    { 9, "cashier05", new DateTime(2026, 10, 4, 13, 30, 0, 0, DateTimeKind.Unspecified), null, 41000m, "HD20261004-002", 0, 3 },
                    { 10, "cashier05", new DateTime(2026, 10, 5, 10, 50, 0, 0, DateTimeKind.Unspecified), null, 168000m, "HD20261005-001", 0, 4 },
                    { 11, "cashier06", new DateTime(2026, 10, 5, 17, 15, 0, 0, DateTimeKind.Unspecified), null, 36000m, "HD20261005-002", 0, 8 },
                    { 12, "cashier06", new DateTime(2026, 10, 6, 8, 20, 0, 0, DateTimeKind.Unspecified), null, 108000m, "HD20261006-001", 0, 2 },
                    { 13, "cashier07", new DateTime(2026, 10, 6, 15, 0, 0, 0, DateTimeKind.Unspecified), null, 78000m, "HD20261006-002", 0, 3 },
                    { 14, "cashier07", new DateTime(2026, 10, 7, 12, 10, 0, 0, DateTimeKind.Unspecified), null, 125000m, "HD20261007-001", 0, 1 },
                    { 15, "cashier01", new DateTime(2026, 10, 7, 18, 45, 0, 0, DateTimeKind.Unspecified), null, 40000m, "HD20261007-002", 0, 4 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "FullName", "IsActive", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "Nhân viên mẫu 01", true, "admin@123", "Admin", "admin" },
                    { 2, "Nhân viên mẫu 02", true, "manager@123", "Admin", "manager01" },
                    { 3, "Nhân viên mẫu 03", true, "cashier@123", "Cashier", "cashier01" },
                    { 4, "Nhân viên mẫu 04", true, "cashier@123", "Cashier", "cashier02" },
                    { 5, "Nhân viên mẫu 05", true, "cashier@123", "Cashier", "cashier03" },
                    { 6, "Nhân viên mẫu 06", true, "cashier@123", "Cashier", "cashier04" },
                    { 7, "Nhân viên mẫu 07", true, "cashier@123", "Cashier", "cashier05" },
                    { 8, "Nhân viên mẫu 08", true, "cashier@123", "Cashier", "cashier06" },
                    { 9, "Nhân viên mẫu 09", true, "cashier@123", "Cashier", "cashier07" },
                    { 10, "Nhân viên mẫu 10", true, "wh@123", "Warehouse", "warehouse01" },
                    { 11, "Nhân viên mẫu 11", true, "wh@123", "Warehouse", "warehouse02" },
                    { 12, "Nhân viên mẫu 12", true, "wh@123", "Warehouse", "warehouse03" },
                    { 13, "Nhân viên mẫu 13", true, "cashier@123", "Cashier", "cashier08" },
                    { 14, "Nhân viên mẫu 14", true, "cashier@123", "Cashier", "cashier09" },
                    { 15, "Nhân viên mẫu 15", false, "lock@123", "Cashier", "staff_lock" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "DataSource", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "893456789001", 1, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 12500m, "Snack Khoai Tây O'Star Gói 60g", 150 },
                    { 2, "893456789002", 1, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 135000m, "Bánh Quy Bơ Danisa Hộp Thiếc 454g", 40 },
                    { 3, "893456789003", 1, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 15000m, "Kẹo Dẻo Chupa Chups Hương Trái Cây 90g", 120 },
                    { 4, "893456789004", 1, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 54000m, "Bánh Chocopie Orion Hộp 12 Cái", 60 },
                    { 5, "893456789005", 2, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 10000m, "Nước Ngọt Coca Cola Lon 320ml", 320 },
                    { 6, "893456789006", 2, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 6000m, "Nước Khoáng Thiên Nhiên Lavie Chai 500ml", 200 },
                    { 7, "893456789007", 2, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 11000m, "Trà Xanh Không Độ Chai 455ml", 180 },
                    { 8, "893456789008", 2, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 14500m, "Nước Tăng Lực Red Bull Lon 250ml", 150 },
                    { 9, "893456789009", 3, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 34500m, "Sữa Tươi Tiệt Trùng Vinamilk Có Đường 1L", 85 },
                    { 10, "893456789010", 3, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 39000m, "Phô Mai Con Bò Cười Hộp 8 Miếng", 60 },
                    { 11, "893456789011", 3, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 28000m, "Sữa Chua Ăn Vinamilk Có Đường Lốc 4 Hộp", 95 },
                    { 12, "893456789012", 3, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 26000m, "Sữa Đặc Có Đường Ông Thọ Lon 380g", 110 },
                    { 13, "893456789013", 4, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 4500m, "Mì Tôm Hảo Hảo Chua Cay Gói 75g", 500 },
                    { 14, "893456789014", 4, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 8500m, "Phở Bò Đệ Nhất Gói 68g", 140 },
                    { 15, "893456789015", 4, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 14000m, "Mì Trộn Tương Đen Koreno Gói 115g", 80 },
                    { 16, "893456789016", 5, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 125000m, "Dầu Ăn Simply Nguyên Chất Can 2L", 45 },
                    { 17, "893456789017", 5, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 30000m, "Nước Mắm Nam Ngư Đệ Nhị Chai 900ml", 90 },
                    { 18, "893456789018", 5, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 36000m, "Hạt Nêm Knorr Từ Thịt Thăn Gói 400g", 75 },
                    { 19, "893456789019", 6, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 19000m, "Cá Nục Sốt Cà Ba Cô Gái Hộp 155g", 130 },
                    { 20, "893456789020", 7, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 29000m, "Nước Rửa Chén Sunlight Chanh Chai 750g", 80 },
                    { 21, "TA-REF-0021", 1, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 18000m, "Bánh quy sữa gói 100g", 0 },
                    { 22, "TA-REF-0022", 1, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 22000m, "Bánh cracker gói 150g", 0 },
                    { 23, "TA-REF-0023", 1, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 25000m, "Bánh gạo gói 150g", 0 },
                    { 24, "TA-REF-0024", 1, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 15000m, "Kẹo bạc hà gói 100g", 0 },
                    { 25, "TA-REF-0025", 1, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 18000m, "Kẹo sữa gói 100g", 0 },
                    { 26, "TA-REF-0026", 1, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 65000m, "Hạt điều rang hộp 200g", 0 },
                    { 27, "TA-REF-0027", 1, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 12000m, "Đậu phộng rang gói 100g", 0 },
                    { 28, "TA-REF-0028", 1, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 15000m, "Rong biển ăn liền gói 20g", 0 },
                    { 29, "TA-REF-0029", 2, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 10000m, "Nước tinh khiết chai 1.5L", 0 },
                    { 30, "TA-REF-0030", 2, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 5000m, "Nước khoáng chai 350ml", 0 },
                    { 31, "TA-REF-0031", 2, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 12000m, "Trà đào chai 450ml", 0 },
                    { 32, "TA-REF-0032", 2, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 12000m, "Trà ô long chai 450ml", 0 },
                    { 33, "TA-REF-0033", 2, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 15000m, "Nước cam chai 350ml", 0 },
                    { 34, "TA-REF-0034", 2, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 10000m, "Nước chanh chai 350ml", 0 },
                    { 35, "TA-REF-0035", 2, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 35000m, "Nước dừa hộp 1L", 0 },
                    { 36, "TA-REF-0036", 2, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 10000m, "Nước yến lon 190ml", 0 },
                    { 37, "TA-REF-0037", 3, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 8000m, "Sữa tươi không đường hộp 180ml", 0 },
                    { 38, "TA-REF-0038", 3, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 8000m, "Sữa tươi có đường hộp 180ml", 0 },
                    { 39, "TA-REF-0039", 3, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 28000m, "Sữa đậu nành hộp 1L", 0 },
                    { 40, "TA-REF-0040", 3, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 9000m, "Sữa chua uống chai 180ml", 0 },
                    { 41, "TA-REF-0041", 3, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 6500m, "Sữa chua không đường hộp 100g", 0 },
                    { 42, "TA-REF-0042", 3, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 180000m, "Sữa bột hộp 400g", 0 },
                    { 43, "TA-REF-0043", 3, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 60000m, "Bơ lạt gói 200g", 0 },
                    { 44, "TA-REF-0044", 3, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 55000m, "Phô mai lát gói 200g", 0 },
                    { 45, "TA-REF-0045", 4, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 4500m, "Mì bò gói 75g", 0 },
                    { 46, "TA-REF-0046", 4, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 4500m, "Mì chay gói 75g", 0 },
                    { 47, "TA-REF-0047", 4, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 7000m, "Cháo thịt gói 50g", 0 },
                    { 48, "TA-REF-0048", 4, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 10000m, "Miến gà gói 58g", 0 },
                    { 49, "TA-REF-0049", 4, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 12000m, "Bún bò gói 75g", 0 },
                    { 50, "TA-REF-0050", 4, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 8500m, "Hủ tiếu gói 65g", 0 },
                    { 51, "TA-REF-0051", 4, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 8500m, "Phở gà gói 65g", 0 },
                    { 52, "TA-REF-0052", 4, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 22000m, "Nui khô gói 400g", 0 },
                    { 53, "TA-REF-0053", 5, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 26000m, "Đường cát trắng túi 1kg", 0 },
                    { 54, "TA-REF-0054", 5, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 8000m, "Muối iốt gói 500g", 0 },
                    { 55, "TA-REF-0055", 5, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 32000m, "Bột ngọt gói 400g", 0 },
                    { 56, "TA-REF-0056", 5, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 18000m, "Tiêu xay lọ 50g", 0 },
                    { 57, "TA-REF-0057", 5, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 22000m, "Nước tương chai 500ml", 0 },
                    { 58, "TA-REF-0058", 5, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 15000m, "Tương ớt chai 250g", 0 },
                    { 59, "TA-REF-0059", 5, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 14000m, "Giấm gạo chai 500ml", 0 },
                    { 60, "TA-REF-0060", 5, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 28000m, "Dầu hào chai 350g", 0 },
                    { 61, "TA-REF-0061", 6, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 35000m, "Cá ngừ ngâm dầu hộp 185g", 0 },
                    { 62, "TA-REF-0062", 6, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 25000m, "Pate gan hộp 150g", 0 },
                    { 63, "TA-REF-0063", 6, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 40000m, "Thịt heo hộp 170g", 0 },
                    { 64, "TA-REF-0064", 6, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 26000m, "Bắp ngọt hộp 340g", 0 },
                    { 65, "TA-REF-0065", 6, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 28000m, "Đậu Hà Lan hộp 400g", 0 },
                    { 66, "TA-REF-0066", 6, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 32000m, "Xúc xích gói 200g", 0 },
                    { 67, "TA-REF-0067", 6, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 55000m, "Chà bông heo hộp 100g", 0 },
                    { 68, "TA-REF-0068", 6, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 35000m, "Dưa leo muối lọ 500g", 0 },
                    { 69, "TA-REF-0069", 7, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 95000m, "Nước giặt túi 2L", 0 },
                    { 70, "TA-REF-0070", 7, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 65000m, "Nước xả vải túi 1.6L", 0 },
                    { 71, "TA-REF-0071", 7, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 35000m, "Bột giặt túi 800g", 0 },
                    { 72, "TA-REF-0072", 7, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 32000m, "Nước lau sàn chai 1L", 0 },
                    { 73, "TA-REF-0073", 7, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 38000m, "Nước tẩy nhà vệ sinh chai 900ml", 0 },
                    { 74, "TA-REF-0074", 7, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 25000m, "Nước lau kính chai 500ml", 0 },
                    { 75, "TA-REF-0075", 7, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 28000m, "Nước rửa tay chai 250ml", 0 },
                    { 76, "TA-REF-0076", 7, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 12000m, "Miếng rửa chén bộ 3 miếng", 0 },
                    { 77, "TA-REF-0077", 8, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 75000m, "Dầu gội chai 340ml", 0 },
                    { 78, "TA-REF-0078", 8, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 85000m, "Sữa tắm chai 500ml", 0 },
                    { 79, "TA-REF-0079", 8, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 35000m, "Kem đánh răng tuýp 180g", 0 },
                    { 80, "TA-REF-0080", 8, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 18000m, "Bàn chải đánh răng chiếc", 0 },
                    { 81, "TA-REF-0081", 8, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 12000m, "Xà phòng bánh 90g", 0 },
                    { 82, "TA-REF-0082", 8, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 25000m, "Khăn ướt gói 80 tờ", 0 },
                    { 83, "TA-REF-0083", 8, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 28000m, "Băng vệ sinh gói 8 miếng", 0 },
                    { 84, "TA-REF-0084", 8, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 15000m, "Tăm bông hộp 200 que", 0 },
                    { 85, "TA-REF-0085", 9, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 22000m, "Khăn giấy hộp 180 tờ", 0 },
                    { 86, "TA-REF-0086", 9, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 55000m, "Giấy vệ sinh lốc 10 cuộn", 0 },
                    { 87, "TA-REF-0087", 9, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 25000m, "Túi rác cuộn 500g", 0 },
                    { 88, "TA-REF-0088", 9, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 28000m, "Màng bọc thực phẩm cuộn 30m", 0 },
                    { 89, "TA-REF-0089", 9, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 25000m, "Giấy bạc cuộn 5m", 0 },
                    { 90, "TA-REF-0090", 9, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 15000m, "Pin AA vỉ 2 viên", 0 },
                    { 91, "TA-REF-0091", 9, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 15000m, "Pin AAA vỉ 2 viên", 0 },
                    { 92, "TA-REF-0092", 9, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 20000m, "Găng tay thực phẩm hộp 100 chiếc", 0 },
                    { 93, "TA-REF-0093", 13, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 55000m, "Cà phê hòa tan hộp 20 gói", 0 },
                    { 94, "TA-REF-0094", 13, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 65000m, "Cà phê rang xay gói 250g", 0 },
                    { 95, "TA-REF-0095", 13, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 45000m, "Yến mạch túi 500g", 0 },
                    { 96, "TA-REF-0096", 13, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 48000m, "Ngũ cốc dinh dưỡng túi 400g", 0 },
                    { 97, "TA-REF-0097", 13, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 35000m, "Trà túi lọc hộp 25 túi", 0 },
                    { 98, "TA-REF-0098", 13, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 55000m, "Bột cacao hộp 200g", 0 },
                    { 99, "TA-REF-0099", 13, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 38000m, "Bột đậu xanh túi 500g", 0 },
                    { 100, "TA-REF-0100", 13, "Tham khảo thực hành; chưa xác minh từ cửa hàng", 95000m, "Gạo trắng túi 5kg", 0 }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderDetailId", "LineTotal", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 10000m, 1, 5, 1, 10000m },
                    { 2, 12500m, 1, 1, 1, 12500m },
                    { 3, 34500m, 1, 9, 1, 34500m },
                    { 4, 135000m, 2, 2, 1, 135000m },
                    { 5, 10000m, 2, 5, 1, 10000m },
                    { 6, 45000m, 3, 13, 10, 4500m },
                    { 7, 138000m, 4, 9, 4, 34500m },
                    { 8, 34500m, 5, 9, 1, 34500m },
                    { 9, 34500m, 5, 9, 1, 34500m },
                    { 10, 53500m, 6, 4, 1, 53500m },
                    { 11, 30000m, 7, 6, 5, 6000m },
                    { 12, 125000m, 8, 16, 1, 125000m },
                    { 13, 30000m, 8, 17, 1, 30000m },
                    { 14, 30000m, 9, 3, 2, 15000m },
                    { 15, 11000m, 9, 7, 1, 11000m },
                    { 16, 108000m, 10, 4, 2, 54000m },
                    { 17, 60000m, 10, 18, 2, 30000m },
                    { 18, 125000m, 14, 16, 1, 125000m },
                    { 19, 36000m, 11, 13, 8, 4500m },
                    { 20, 108000m, 12, 4, 2, 54000m },
                    { 21, 78000m, 13, 12, 3, 26000m },
                    { 22, 40000m, 15, 5, 4, 10000m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PhoneNumber",
                table: "Customers",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderCode",
                table: "Orders",
                column: "OrderCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Barcode",
                table: "Products",
                column: "Barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
