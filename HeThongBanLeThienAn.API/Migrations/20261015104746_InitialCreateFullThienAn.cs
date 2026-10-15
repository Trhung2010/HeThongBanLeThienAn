using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HeThongBanLeThienAn.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateFullThienAn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    RewardPoints = table.Column<int>(type: "INTEGER", nullable: false),
                    MembershipRank = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Barcode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    CashierUsername = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    TotalItems = table.Column<int>(type: "INTEGER", nullable: false),
                    FinalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RewardPoints = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
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
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[,]
                {
                    { 1, "123 Lê Lợi, Quận 1, TPHCM", "Trần Văn Hoàng", "Bạc", "0903123456", 120 },
                    { 2, "45 Điện Biên Phủ, Quận Bình Thạnh, TPHCM", "Nguyễn Thị Kim Loan", "Vàng", "0918234567", 350 },
                    { 3, "88 Nam Kỳ Khởi Nghĩa, Quận 3, TPHCM", "Lê Minh Trí", "Chuẩn", "0977345678", 45 },
                    { 4, "102 Quang Trung, Quận Gò Vấp, TPHCM", "Phạm Hồng Nhung", "Bạch Kim", "0988456789", 520 },
                    { 5, "15 Phan Xích Long, Quận Phú Nhuận, TPHCM", "Võ Thành Nam", "Chuẩn", "0909567890", 80 },
                    { 6, "234 Hoàng Văn Thụ, Quận Tân Bình, TPHCM", "Đỗ Thị Mỹ Duyên", "Bạc", "0933678901", 210 },
                    { 7, "312 Ba Tháng Hai, Quận 10, TPHCM", "Hoàng Quốc Bảo", "Chuẩn", "0944789012", 15 },
                    { 8, "56 Nguyễn Thị Thập, Quận 7, TPHCM", "Bùi Lan Hương", "Vàng", "0966890123", 410 },
                    { 9, "79 Kinh Dương Vương, Quận Bình Tân, TPHCM", "Đặng Tuấn Kiệt", "Chuẩn", "0922901234", 95 },
                    { 10, "18 Hoàng Diệu, Quận 4, TPHCM", "Ngô Thanh Vân", "Bạch Kim", "0971012345", 650 },
                    { 11, "405 Lê Văn Khương, Quận 12, TPHCM", "Trương Đình Trọng", "Chuẩn", "0982123456", 30 },
                    { 12, "123 Long Thuận, TP. Thủ Đức, TPHCM", "Lý Mỹ Lệ", "Bạc", "0904234567", 180 },
                    { 13, "67 Lũy Bán Bích, Quận Tân Phú, TPHCM", "Vũ Đức Thịnh", "Bạc", "0915345678", 290 },
                    { 14, "142 Phạm Thế Hiển, Quận 8, TPHCM", "Phan Bích Thủy", "Vàng", "0936456789", 480 },
                    { 15, "89 An Dương Vương, Quận 5, TPHCM", "Dương Gia Huy", "Chuẩn", "0977567890", 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "FullName", "IsActive", "PasswordHash", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "Nguyễn Quản Trị", true, "admin@123", "Admin", "admin" },
                    { 2, "Trần Cửa Hàng Trưởng", true, "manager@123", "Admin", "manager01" },
                    { 3, "Lê Thu Ngân 1", true, "cashier@123", "Cashier", "cashier01" },
                    { 4, "Phạm Thu Ngân 2", true, "cashier@123", "Cashier", "cashier02" },
                    { 5, "Hoàng Thu Ngân 3", true, "cashier@123", "Cashier", "cashier03" },
                    { 6, "Vũ Thu Ngân 4", true, "cashier@123", "Cashier", "cashier04" },
                    { 7, "Đỗ Thu Ngân 5", true, "cashier@123", "Cashier", "cashier05" },
                    { 8, "Ngô Thu Ngân 6", true, "cashier@123", "Cashier", "cashier06" },
                    { 9, "Bùi Thu Ngân 7", true, "cashier@123", "Cashier", "cashier07" },
                    { 10, "Đặng Thủ Kho 1", true, "wh@123", "Warehouse", "warehouse01" },
                    { 11, "Trương Thủ Kho 2", true, "wh@123", "Warehouse", "warehouse02" },
                    { 12, "Lý Kiểm Kê", true, "wh@123", "Warehouse", "warehouse03" },
                    { 13, "Mai Thu Ngân 8", true, "cashier@123", "Cashier", "cashier08" },
                    { 14, "Dương Thu Ngân 9", true, "cashier@123", "Cashier", "cashier09" },
                    { 15, "Hồ Nhân Viên Nghỉ Việc", false, "lock@123", "Cashier", "staff_lock" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CashierUsername", "CreatedDate", "CustomerId", "FinalAmount", "OrderCode", "RewardPoints", "TotalItems" },
                values: new object[,]
                {
                    { 1, "cashier01", new DateTime(2026, 10, 1, 8, 30, 0, 0, DateTimeKind.Unspecified), 1, 57000m, "HD20261001-001", 5, 3 },
                    { 2, "cashier01", new DateTime(2026, 10, 1, 9, 15, 0, 0, DateTimeKind.Unspecified), 2, 145000m, "HD20261001-002", 14, 2 },
                    { 3, "cashier02", new DateTime(2026, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 3, 45000m, "HD20261001-003", 4, 10 },
                    { 4, "cashier02", new DateTime(2026, 10, 2, 8, 45, 0, 0, DateTimeKind.Unspecified), 4, 138000m, "HD20261002-001", 13, 4 },
                    { 5, "cashier03", new DateTime(2026, 10, 2, 11, 20, 0, 0, DateTimeKind.Unspecified), 5, 69000m, "HD20261002-002", 6, 2 },
                    { 6, "cashier03", new DateTime(2026, 10, 3, 14, 10, 0, 0, DateTimeKind.Unspecified), 6, 53500m, "HD20261003-001", 5, 3 },
                    { 7, "cashier04", new DateTime(2026, 10, 3, 16, 40, 0, 0, DateTimeKind.Unspecified), 7, 30000m, "HD20261003-002", 3, 5 },
                    { 8, "cashier04", new DateTime(2026, 10, 4, 9, 5, 0, 0, DateTimeKind.Unspecified), 8, 155000m, "HD20261004-001", 15, 2 },
                    { 9, "cashier05", new DateTime(2026, 10, 4, 13, 30, 0, 0, DateTimeKind.Unspecified), 9, 48500m, "HD20261004-002", 4, 4 },
                    { 10, "cashier05", new DateTime(2026, 10, 5, 10, 50, 0, 0, DateTimeKind.Unspecified), 10, 168000m, "HD20261005-001", 16, 6 },
                    { 11, "cashier06", new DateTime(2026, 10, 5, 17, 15, 0, 0, DateTimeKind.Unspecified), 11, 36000m, "HD20261005-002", 3, 8 },
                    { 12, "cashier06", new DateTime(2026, 10, 6, 8, 20, 0, 0, DateTimeKind.Unspecified), 12, 108000m, "HD20261006-001", 10, 2 },
                    { 13, "cashier07", new DateTime(2026, 10, 6, 15, 0, 0, 0, DateTimeKind.Unspecified), 13, 78000m, "HD20261006-002", 7, 3 },
                    { 14, "cashier07", new DateTime(2026, 10, 7, 12, 10, 0, 0, DateTimeKind.Unspecified), 14, 125000m, "HD20261007-001", 12, 1 },
                    { 15, "cashier01", new DateTime(2026, 10, 7, 18, 45, 0, 0, DateTimeKind.Unspecified), 15, 40000m, "HD20261007-002", 4, 4 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "893456789001", 1, 12500m, "Snack Khoai Tây O'Star Gói 60g", 150 },
                    { 2, "893456789002", 1, 135000m, "Bánh Quy Bơ Danisa Hộp Thiếc 454g", 40 },
                    { 3, "893456789003", 1, 15000m, "Kẹo Dẻo Chupa Chups Hương Trái Cây 90g", 120 },
                    { 4, "893456789004", 1, 54000m, "Bánh Chocopie Orion Hộp 12 Cái", 60 },
                    { 5, "893456789005", 2, 10000m, "Nước Ngọt Coca Cola Lon 320ml", 320 },
                    { 6, "893456789006", 2, 6000m, "Nước Khoáng Thiên Nhiên Lavie Chai 500ml", 200 },
                    { 7, "893456789007", 2, 11000m, "Trà Xanh Không Độ Chai 455ml", 180 },
                    { 8, "893456789008", 2, 14500m, "Nước Tăng Lực Red Bull Lon 250ml", 150 },
                    { 9, "893456789009", 3, 34500m, "Sữa Tươi Tiệt Trùng Vinamilk Có Đường 1L", 85 },
                    { 10, "893456789010", 3, 39000m, "Phô Mai Con Bò Cười Hộp 8 Miếng", 60 },
                    { 11, "893456789011", 3, 28000m, "Sữa Chua Ăn Vinamilk Có Đường Lốc 4 Hộp", 95 },
                    { 12, "893456789012", 3, 26000m, "Sữa Đặc Có Đường Ông Thọ Lon 380g", 110 },
                    { 13, "893456789013", 4, 4500m, "Mì Tôm Hảo Hảo Chua Cay Gói 75g", 500 },
                    { 14, "893456789014", 4, 8500m, "Phở Bò Đệ Nhất Gói 68g", 140 },
                    { 15, "893456789015", 4, 14000m, "Mì Trộn Tương Đen Koreno Gói 115g", 80 },
                    { 16, "893456789016", 5, 125000m, "Dầu Ăn Simply Nguyên Chất Can 2L", 45 },
                    { 17, "893456789017", 5, 30000m, "Nước Mắm Nam Ngư Đệ Nhị Chai 900ml", 90 },
                    { 18, "893456789018", 5, 36000m, "Hạt Nêm Knorr Từ Thịt Thăn Gói 400g", 75 },
                    { 19, "893456789019", 6, 19000m, "Cá Nục Sốt Cà Ba Cô Gái Hộp 155g", 130 },
                    { 20, "893456789020", 7, 29000m, "Nước Rửa Chén Sunlight Chanh Chai 750g", 80 }
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
                    { 18, 125000m, 14, 16, 1, 125000m }
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
