using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HeThongBanLeThienAn.API.Migrations
{
    /// <inheritdoc />
    public partial class ReferenceCatalogAndSafeCheckout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "DataSource",
                table: "Products",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Orders",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.Sql("UPDATE Customers SET Address = 'Dữ liệu thực hành, không phải khách thật', CustomerName = 'Khách mẫu 01', PhoneNumber = 'DEMO-KH-001' WHERE CustomerId = 1 AND CustomerName = 'Trần Văn Hoàng' AND PhoneNumber = '0903123456' AND Address = '123 Lê Lợi, Quận 1, TPHCM';");

            migrationBuilder.Sql("UPDATE Customers SET Address = 'Dữ liệu thực hành, không phải khách thật', CustomerName = 'Khách mẫu 02', PhoneNumber = 'DEMO-KH-002' WHERE CustomerId = 2 AND CustomerName = 'Nguyễn Thị Kim Loan' AND PhoneNumber = '0918234567' AND Address = '45 Điện Biên Phủ, Quận Bình Thạnh, TPHCM';");

            migrationBuilder.Sql("UPDATE Customers SET Address = 'Dữ liệu thực hành, không phải khách thật', CustomerName = 'Khách mẫu 03', PhoneNumber = 'DEMO-KH-003' WHERE CustomerId = 3 AND CustomerName = 'Lê Minh Trí' AND PhoneNumber = '0977345678' AND Address = '88 Nam Kỳ Khởi Nghĩa, Quận 3, TPHCM';");

            migrationBuilder.Sql("UPDATE Customers SET Address = 'Dữ liệu thực hành, không phải khách thật', CustomerName = 'Khách mẫu 04', PhoneNumber = 'DEMO-KH-004' WHERE CustomerId = 4 AND CustomerName = 'Phạm Hồng Nhung' AND PhoneNumber = '0988456789' AND Address = '102 Quang Trung, Quận Gò Vấp, TPHCM';");

            migrationBuilder.Sql("UPDATE Customers SET Address = 'Dữ liệu thực hành, không phải khách thật', CustomerName = 'Khách mẫu 05', PhoneNumber = 'DEMO-KH-005' WHERE CustomerId = 5 AND CustomerName = 'Võ Thành Nam' AND PhoneNumber = '0909567890' AND Address = '15 Phan Xích Long, Quận Phú Nhuận, TPHCM';");

            migrationBuilder.Sql("UPDATE Customers SET Address = 'Dữ liệu thực hành, không phải khách thật', CustomerName = 'Khách mẫu 06', PhoneNumber = 'DEMO-KH-006' WHERE CustomerId = 6 AND CustomerName = 'Đỗ Thị Mỹ Duyên' AND PhoneNumber = '0933678901' AND Address = '234 Hoàng Văn Thụ, Quận Tân Bình, TPHCM';");

            migrationBuilder.Sql("UPDATE Customers SET Address = 'Dữ liệu thực hành, không phải khách thật', CustomerName = 'Khách mẫu 07', PhoneNumber = 'DEMO-KH-007' WHERE CustomerId = 7 AND CustomerName = 'Hoàng Quốc Bảo' AND PhoneNumber = '0944789012' AND Address = '312 Ba Tháng Hai, Quận 10, TPHCM';");

            migrationBuilder.Sql("UPDATE Customers SET Address = 'Dữ liệu thực hành, không phải khách thật', CustomerName = 'Khách mẫu 08', PhoneNumber = 'DEMO-KH-008' WHERE CustomerId = 8 AND CustomerName = 'Bùi Lan Hương' AND PhoneNumber = '0966890123' AND Address = '56 Nguyễn Thị Thập, Quận 7, TPHCM';");

            migrationBuilder.Sql("UPDATE Customers SET Address = 'Dữ liệu thực hành, không phải khách thật', CustomerName = 'Khách mẫu 09', PhoneNumber = 'DEMO-KH-009' WHERE CustomerId = 9 AND CustomerName = 'Đặng Tuấn Kiệt' AND PhoneNumber = '0922901234' AND Address = '79 Kinh Dương Vương, Quận Bình Tân, TPHCM';");

            migrationBuilder.Sql("UPDATE Customers SET Address = 'Dữ liệu thực hành, không phải khách thật', CustomerName = 'Khách mẫu 10', PhoneNumber = 'DEMO-KH-010' WHERE CustomerId = 10 AND CustomerName = 'Ngô Thanh Vân' AND PhoneNumber = '0971012345' AND Address = '18 Hoàng Diệu, Quận 4, TPHCM';");

            migrationBuilder.Sql("UPDATE Customers SET Address = 'Dữ liệu thực hành, không phải khách thật', CustomerName = 'Khách mẫu 11', PhoneNumber = 'DEMO-KH-011' WHERE CustomerId = 11 AND CustomerName = 'Trương Đình Trọng' AND PhoneNumber = '0982123456' AND Address = '405 Lê Văn Khương, Quận 12, TPHCM';");

            migrationBuilder.Sql("UPDATE Customers SET Address = 'Dữ liệu thực hành, không phải khách thật', CustomerName = 'Khách mẫu 12', PhoneNumber = 'DEMO-KH-012' WHERE CustomerId = 12 AND CustomerName = 'Lý Mỹ Lệ' AND PhoneNumber = '0904234567' AND Address = '123 Long Thuận, TP. Thủ Đức, TPHCM';");

            migrationBuilder.Sql("UPDATE Customers SET Address = 'Dữ liệu thực hành, không phải khách thật', CustomerName = 'Khách mẫu 13', PhoneNumber = 'DEMO-KH-013' WHERE CustomerId = 13 AND CustomerName = 'Vũ Đức Thịnh' AND PhoneNumber = '0915345678' AND Address = '67 Lũy Bán Bích, Quận Tân Phú, TPHCM';");

            migrationBuilder.Sql("UPDATE Customers SET Address = 'Dữ liệu thực hành, không phải khách thật', CustomerName = 'Khách mẫu 14', PhoneNumber = 'DEMO-KH-014' WHERE CustomerId = 14 AND CustomerName = 'Phan Bích Thủy' AND PhoneNumber = '0936456789' AND Address = '142 Phạm Thế Hiển, Quận 8, TPHCM';");

            migrationBuilder.Sql("UPDATE Customers SET Address = 'Dữ liệu thực hành, không phải khách thật', CustomerName = 'Khách mẫu 15', PhoneNumber = 'DEMO-KH-015' WHERE CustomerId = 15 AND CustomerName = 'Dương Gia Huy' AND PhoneNumber = '0977567890' AND Address = '89 An Dương Vương, Quận 5, TPHCM';");

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderDetailId", "LineTotal", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 19, 36000m, 11, 13, 8, 4500m },
                    { 20, 108000m, 12, 4, 2, 54000m },
                    { 21, 78000m, 13, 12, 3, 26000m },
                    { 22, 40000m, 15, 5, 4, 10000m }
                });

            migrationBuilder.Sql("UPDATE Orders SET TotalItems = 1 WHERE OrderId = 6 AND OrderCode = 'HD20261003-001';");

            migrationBuilder.Sql("UPDATE Orders SET FinalAmount = 41000, TotalItems = 3 WHERE OrderId = 9 AND OrderCode = 'HD20261004-002';");

            migrationBuilder.Sql("UPDATE Orders SET TotalItems = 4 WHERE OrderId = 10 AND OrderCode = 'HD20261005-001';");

            migrationBuilder.Sql("UPDATE Products SET DataSource = 'Tham khảo thực hành; chưa xác minh từ cửa hàng' WHERE ProductId = 1 AND Barcode = '893456789001' AND ProductName = 'Snack Khoai Tây O''Star Gói 60g';");

            migrationBuilder.Sql("UPDATE Products SET DataSource = 'Tham khảo thực hành; chưa xác minh từ cửa hàng' WHERE ProductId = 2 AND Barcode = '893456789002' AND ProductName = 'Bánh Quy Bơ Danisa Hộp Thiếc 454g';");

            migrationBuilder.Sql("UPDATE Products SET DataSource = 'Tham khảo thực hành; chưa xác minh từ cửa hàng' WHERE ProductId = 3 AND Barcode = '893456789003' AND ProductName = 'Kẹo Dẻo Chupa Chups Hương Trái Cây 90g';");

            migrationBuilder.Sql("UPDATE Products SET DataSource = 'Tham khảo thực hành; chưa xác minh từ cửa hàng' WHERE ProductId = 4 AND Barcode = '893456789004' AND ProductName = 'Bánh Chocopie Orion Hộp 12 Cái';");

            migrationBuilder.Sql("UPDATE Products SET DataSource = 'Tham khảo thực hành; chưa xác minh từ cửa hàng' WHERE ProductId = 5 AND Barcode = '893456789005' AND ProductName = 'Nước Ngọt Coca Cola Lon 320ml';");

            migrationBuilder.Sql("UPDATE Products SET DataSource = 'Tham khảo thực hành; chưa xác minh từ cửa hàng' WHERE ProductId = 6 AND Barcode = '893456789006' AND ProductName = 'Nước Khoáng Thiên Nhiên Lavie Chai 500ml';");

            migrationBuilder.Sql("UPDATE Products SET DataSource = 'Tham khảo thực hành; chưa xác minh từ cửa hàng' WHERE ProductId = 7 AND Barcode = '893456789007' AND ProductName = 'Trà Xanh Không Độ Chai 455ml';");

            migrationBuilder.Sql("UPDATE Products SET DataSource = 'Tham khảo thực hành; chưa xác minh từ cửa hàng' WHERE ProductId = 8 AND Barcode = '893456789008' AND ProductName = 'Nước Tăng Lực Red Bull Lon 250ml';");

            migrationBuilder.Sql("UPDATE Products SET DataSource = 'Tham khảo thực hành; chưa xác minh từ cửa hàng' WHERE ProductId = 9 AND Barcode = '893456789009' AND ProductName = 'Sữa Tươi Tiệt Trùng Vinamilk Có Đường 1L';");

            migrationBuilder.Sql("UPDATE Products SET DataSource = 'Tham khảo thực hành; chưa xác minh từ cửa hàng' WHERE ProductId = 10 AND Barcode = '893456789010' AND ProductName = 'Phô Mai Con Bò Cười Hộp 8 Miếng';");

            migrationBuilder.Sql("UPDATE Products SET DataSource = 'Tham khảo thực hành; chưa xác minh từ cửa hàng' WHERE ProductId = 11 AND Barcode = '893456789011' AND ProductName = 'Sữa Chua Ăn Vinamilk Có Đường Lốc 4 Hộp';");

            migrationBuilder.Sql("UPDATE Products SET DataSource = 'Tham khảo thực hành; chưa xác minh từ cửa hàng' WHERE ProductId = 12 AND Barcode = '893456789012' AND ProductName = 'Sữa Đặc Có Đường Ông Thọ Lon 380g';");

            migrationBuilder.Sql("UPDATE Products SET DataSource = 'Tham khảo thực hành; chưa xác minh từ cửa hàng' WHERE ProductId = 13 AND Barcode = '893456789013' AND ProductName = 'Mì Tôm Hảo Hảo Chua Cay Gói 75g';");

            migrationBuilder.Sql("UPDATE Products SET DataSource = 'Tham khảo thực hành; chưa xác minh từ cửa hàng' WHERE ProductId = 14 AND Barcode = '893456789014' AND ProductName = 'Phở Bò Đệ Nhất Gói 68g';");

            migrationBuilder.Sql("UPDATE Products SET DataSource = 'Tham khảo thực hành; chưa xác minh từ cửa hàng' WHERE ProductId = 15 AND Barcode = '893456789015' AND ProductName = 'Mì Trộn Tương Đen Koreno Gói 115g';");

            migrationBuilder.Sql("UPDATE Products SET DataSource = 'Tham khảo thực hành; chưa xác minh từ cửa hàng' WHERE ProductId = 16 AND Barcode = '893456789016' AND ProductName = 'Dầu Ăn Simply Nguyên Chất Can 2L';");

            migrationBuilder.Sql("UPDATE Products SET DataSource = 'Tham khảo thực hành; chưa xác minh từ cửa hàng' WHERE ProductId = 17 AND Barcode = '893456789017' AND ProductName = 'Nước Mắm Nam Ngư Đệ Nhị Chai 900ml';");

            migrationBuilder.Sql("UPDATE Products SET DataSource = 'Tham khảo thực hành; chưa xác minh từ cửa hàng' WHERE ProductId = 18 AND Barcode = '893456789018' AND ProductName = 'Hạt Nêm Knorr Từ Thịt Thăn Gói 400g';");

            migrationBuilder.Sql("UPDATE Products SET DataSource = 'Tham khảo thực hành; chưa xác minh từ cửa hàng' WHERE ProductId = 19 AND Barcode = '893456789019' AND ProductName = 'Cá Nục Sốt Cà Ba Cô Gái Hộp 155g';");

            migrationBuilder.Sql("UPDATE Products SET DataSource = 'Tham khảo thực hành; chưa xác minh từ cửa hàng' WHERE ProductId = 20 AND Barcode = '893456789020' AND ProductName = 'Nước Rửa Chén Sunlight Chanh Chai 750g';");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "DataSource", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
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

            migrationBuilder.Sql("UPDATE Users SET FullName = 'Nhân viên mẫu 01' WHERE UserId = 1 AND Username = 'admin' AND FullName = 'Nguyễn Quản Trị';");

            migrationBuilder.Sql("UPDATE Users SET FullName = 'Nhân viên mẫu 02' WHERE UserId = 2 AND Username = 'manager01' AND FullName = 'Trần Cửa Hàng Trưởng';");

            migrationBuilder.Sql("UPDATE Users SET FullName = 'Nhân viên mẫu 03' WHERE UserId = 3 AND Username = 'cashier01' AND FullName = 'Lê Thu Ngân 1';");

            migrationBuilder.Sql("UPDATE Users SET FullName = 'Nhân viên mẫu 04' WHERE UserId = 4 AND Username = 'cashier02' AND FullName = 'Phạm Thu Ngân 2';");

            migrationBuilder.Sql("UPDATE Users SET FullName = 'Nhân viên mẫu 05' WHERE UserId = 5 AND Username = 'cashier03' AND FullName = 'Hoàng Thu Ngân 3';");

            migrationBuilder.Sql("UPDATE Users SET FullName = 'Nhân viên mẫu 06' WHERE UserId = 6 AND Username = 'cashier04' AND FullName = 'Vũ Thu Ngân 4';");

            migrationBuilder.Sql("UPDATE Users SET FullName = 'Nhân viên mẫu 07' WHERE UserId = 7 AND Username = 'cashier05' AND FullName = 'Đỗ Thu Ngân 5';");

            migrationBuilder.Sql("UPDATE Users SET FullName = 'Nhân viên mẫu 08' WHERE UserId = 8 AND Username = 'cashier06' AND FullName = 'Ngô Thu Ngân 6';");

            migrationBuilder.Sql("UPDATE Users SET FullName = 'Nhân viên mẫu 09' WHERE UserId = 9 AND Username = 'cashier07' AND FullName = 'Bùi Thu Ngân 7';");

            migrationBuilder.Sql("UPDATE Users SET FullName = 'Nhân viên mẫu 10' WHERE UserId = 10 AND Username = 'warehouse01' AND FullName = 'Đặng Thủ Kho 1';");

            migrationBuilder.Sql("UPDATE Users SET FullName = 'Nhân viên mẫu 11' WHERE UserId = 11 AND Username = 'warehouse02' AND FullName = 'Trương Thủ Kho 2';");

            migrationBuilder.Sql("UPDATE Users SET FullName = 'Nhân viên mẫu 12' WHERE UserId = 12 AND Username = 'warehouse03' AND FullName = 'Lý Kiểm Kê';");

            migrationBuilder.Sql("UPDATE Users SET FullName = 'Nhân viên mẫu 13' WHERE UserId = 13 AND Username = 'cashier08' AND FullName = 'Mai Thu Ngân 8';");

            migrationBuilder.Sql("UPDATE Users SET FullName = 'Nhân viên mẫu 14' WHERE UserId = 14 AND Username = 'cashier09' AND FullName = 'Dương Thu Ngân 9';");

            migrationBuilder.Sql("UPDATE Users SET FullName = 'Nhân viên mẫu 15' WHERE UserId = 15 AND Username = 'staff_lock' AND FullName = 'Hồ Nhân Viên Nghỉ Việc';");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 100);

            migrationBuilder.DropColumn(
                name: "DataSource",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                columns: new[] { "Address", "CustomerName", "PhoneNumber" },
                values: new object[] { "123 Lê Lợi, Quận 1, TPHCM", "Trần Văn Hoàng", "0903123456" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                columns: new[] { "Address", "CustomerName", "PhoneNumber" },
                values: new object[] { "45 Điện Biên Phủ, Quận Bình Thạnh, TPHCM", "Nguyễn Thị Kim Loan", "0918234567" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                columns: new[] { "Address", "CustomerName", "PhoneNumber" },
                values: new object[] { "88 Nam Kỳ Khởi Nghĩa, Quận 3, TPHCM", "Lê Minh Trí", "0977345678" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4,
                columns: new[] { "Address", "CustomerName", "PhoneNumber" },
                values: new object[] { "102 Quang Trung, Quận Gò Vấp, TPHCM", "Phạm Hồng Nhung", "0988456789" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5,
                columns: new[] { "Address", "CustomerName", "PhoneNumber" },
                values: new object[] { "15 Phan Xích Long, Quận Phú Nhuận, TPHCM", "Võ Thành Nam", "0909567890" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6,
                columns: new[] { "Address", "CustomerName", "PhoneNumber" },
                values: new object[] { "234 Hoàng Văn Thụ, Quận Tân Bình, TPHCM", "Đỗ Thị Mỹ Duyên", "0933678901" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7,
                columns: new[] { "Address", "CustomerName", "PhoneNumber" },
                values: new object[] { "312 Ba Tháng Hai, Quận 10, TPHCM", "Hoàng Quốc Bảo", "0944789012" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8,
                columns: new[] { "Address", "CustomerName", "PhoneNumber" },
                values: new object[] { "56 Nguyễn Thị Thập, Quận 7, TPHCM", "Bùi Lan Hương", "0966890123" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 9,
                columns: new[] { "Address", "CustomerName", "PhoneNumber" },
                values: new object[] { "79 Kinh Dương Vương, Quận Bình Tân, TPHCM", "Đặng Tuấn Kiệt", "0922901234" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 10,
                columns: new[] { "Address", "CustomerName", "PhoneNumber" },
                values: new object[] { "18 Hoàng Diệu, Quận 4, TPHCM", "Ngô Thanh Vân", "0971012345" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 11,
                columns: new[] { "Address", "CustomerName", "PhoneNumber" },
                values: new object[] { "405 Lê Văn Khương, Quận 12, TPHCM", "Trương Đình Trọng", "0982123456" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 12,
                columns: new[] { "Address", "CustomerName", "PhoneNumber" },
                values: new object[] { "123 Long Thuận, TP. Thủ Đức, TPHCM", "Lý Mỹ Lệ", "0904234567" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 13,
                columns: new[] { "Address", "CustomerName", "PhoneNumber" },
                values: new object[] { "67 Lũy Bán Bích, Quận Tân Phú, TPHCM", "Vũ Đức Thịnh", "0915345678" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 14,
                columns: new[] { "Address", "CustomerName", "PhoneNumber" },
                values: new object[] { "142 Phạm Thế Hiển, Quận 8, TPHCM", "Phan Bích Thủy", "0936456789" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 15,
                columns: new[] { "Address", "CustomerName", "PhoneNumber" },
                values: new object[] { "89 An Dương Vương, Quận 5, TPHCM", "Dương Gia Huy", "0977567890" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 6,
                column: "TotalItems",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 9,
                columns: new[] { "FinalAmount", "TotalItems" },
                values: new object[] { 48500m, 4 });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 10,
                column: "TotalItems",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "FullName",
                value: "Nguyễn Quản Trị");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "FullName",
                value: "Trần Cửa Hàng Trưởng");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "FullName",
                value: "Lê Thu Ngân 1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "FullName",
                value: "Phạm Thu Ngân 2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                column: "FullName",
                value: "Hoàng Thu Ngân 3");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6,
                column: "FullName",
                value: "Vũ Thu Ngân 4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7,
                column: "FullName",
                value: "Đỗ Thu Ngân 5");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8,
                column: "FullName",
                value: "Ngô Thu Ngân 6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 9,
                column: "FullName",
                value: "Bùi Thu Ngân 7");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 10,
                column: "FullName",
                value: "Đặng Thủ Kho 1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 11,
                column: "FullName",
                value: "Trương Thủ Kho 2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 12,
                column: "FullName",
                value: "Lý Kiểm Kê");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 13,
                column: "FullName",
                value: "Mai Thu Ngân 8");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 14,
                column: "FullName",
                value: "Dương Thu Ngân 9");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 15,
                column: "FullName",
                value: "Hồ Nhân Viên Nghỉ Việc");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
