# Hệ thống bán lẻ Thiên Ân

Ứng dụng thực hành .NET 8 gồm ASP.NET Core Web API, WinForms POS và SQL Server Express.
Đã triển khai nội dung buổi 1–6: CRUD, JWT/phân quyền, EF Core migrations, ADO.NET transaction, phân trang LINQ và WinForms client-server.

## Nguồn dữ liệu

**Bộ dữ liệu tham khảo phục vụ thực hành, chưa xác minh từ website chính thức Bách Hóa Thiên Ân.**

- 100 mặt hàng, mã riêng biệt, 15 danh mục.
- 100 mặt hàng dùng mã vạch số 12 chữ số liên tiếp từ `893456789001` đến `893456789100`. 80 mặt hàng bổ sung có tồn kho ban đầu 0; nhập tồn kho trước khi bán.
- Mã nội bộ không phải barcode nhà sản xuất; giá không phải bảng giá xác nhận của cửa hàng.
- `Products.DataSource` phân biệt `Kho tổng Thiên Ân` với sản phẩm `Nhập thủ công`.
- CSDL mới không tự tạo khách mẫu. Hồ sơ khách được nhập qua API/WinForms. Migration chỉ dọn hồ sơ còn khớp chính xác tên và mã DEMO; giữ nguyên khách đã sửa và hóa đơn hiện có. Nhân viên và hóa đơn seed là dữ liệu thực hành.
- Hóa đơn mẫu có đủ 22 dòng chi tiết, tổng tiền và số lượng được đối soát. Số dư điểm seed là số dư thực hành, không phải sổ điểm của cửa hàng.

## Khởi chạy

```powershell
dotnet run --project HeThongBanLeThienAn.API
dotnet run --project HeThongBanLeThienAn.WinForms
```

API: http://localhost:5167; Swagger: http://localhost:5167/swagger.
Kết nối mặc định: `localhost\\SQLEXPRESS`, database `MiniSupermarketDb`, Windows Authentication.

API áp dụng migration lúc khởi động. **Không dùng EnsureDeleted/EnsureCreated hoặc kiểm tra số lượng bản ghi để reset CSDL.**
Ứng dụng chỉ dùng migration SQL Server trong `Migrations/SqlServer`.

Khởi tạo/cập nhật SQL Server thủ công khi cần:

```powershell
dotnet ef database update --context SqlServerSupermarketDbContext --project HeThongBanLeThienAn.API --startup-project HeThongBanLeThienAn.API
```

Tài khoản demo trong SQL Server:

| Vai trò | Tài khoản | Mật khẩu |
|---|---|---|
| Admin | admin | admin@123 |
| Thu ngân | cashier01 | cashier@123 |
| Thủ kho | warehouse01 | wh@123 |

Mật khẩu demo hiện lưu dạng rõ; chưa triển khai lưu mật khẩu băm. Không dùng tài khoản/mật khẩu demo cho vận hành thực tế.
Đăng nhập qua CSDL không chấp nhận mật khẩu chung 123456 và không quay về danh sách tài khoản RAM khi sai mật khẩu.

## POS và giao dịch

- WinForms gọi `POST /api/checkout`; route cũ `POST /api/orders/checkout` dùng cùng `CheckoutService`.
- Chỉ Admin/Cashier được thanh toán; danh tính thu ngân lấy từ JWT, không tin danh tính gửi trong body.
- Gộp dòng trùng sản phẩm; từ chối số lượng <= 0, thiếu hàng, sản phẩm/khách không tồn tại.
- Giá lấy từ CSDL; giá client không quyết định tiền hóa đơn.
- Tạo hóa đơn, lưu chi tiết, trừ kho, cộng điểm trong một transaction ADO.NET.
- Lỗi bất kỳ hoàn tác tất cả thay đổi. Cập nhật kho có điều kiện để không bán âm.
- Khách vãng lai có CustomerId null, không tích điểm vào thành viên khác.
- Khách có số điện thoại chưa đăng ký cần được tạo hồ sơ hoặc để trống để bán vãng lai.
- Admin hủy đơn qua `DELETE /api/checkout/cancel/{orderId}`: hoàn trả tồn, điểm và hạng thẻ trong một transaction.
- POS khóa thao tác thanh toán khi đang gửi để tránh bấm liên tục; hiển thị số tiền từ kết quả máy chủ.

## Báo cáo và buổi 5

- `GET /api/reports/quick?date=yyyy-MM-dd`: doanh thu/số đơn thực tế theo ngày, hàng bán chạy tính theo tổng số lượng bán; ngày trống trả 0.
- `GET /api/reports/revenue-by-cashier`, `/revenue-by-date`, `/top-selling-products`: tổng hợp qua SQL có tham số, chỉ Admin.
- Khoảng ngày gồm toàn bộ ngày kết thúc, không bỏ giao dịch sau 00:00.
- `GET /api/ado-products/barcode/{barcode}`, `/disconnected-cache`, `/filter-stock`, `/count-by-category`: tra cứu ADO.NET, yêu cầu Admin/Cashier/Warehouse.
- Ba Stored Procedure `sp_GetRevenueByCashier`, `sp_GetRevenueByDateRange` và `sp_GetTop5SellingProducts` được tạo thật bằng migration SQL Server.
- `Data/StoredProcedures_Buoi5.sql` là bản T-SQL có thể mở và trình diễn trực tiếp trong SSMS.

## Buổi 6

- `GET /api/products/paged`: phân trang phía máy chủ bằng `OrderBy`/`Skip`/`Take`, tìm theo tên hoặc barcode, lọc theo danh mục, trả tên danh mục.
- `GET /api/customers/paged`: bài mở rộng phân trang khách hàng theo điểm thưởng giảm dần.
- `FormProductManagement` dùng `ApiClientService` chung có JWT, tải 5 sản phẩm mỗi trang, tìm kiếm, lọc nhóm hàng và CRUD trực tiếp qua API.
- Nút Trang trước/Trang sau bị khóa đúng tại biên và trong lúc request đang chạy để tránh gửi chồng yêu cầu.

## Kiểm thử

```powershell
dotnet test HeThongBanLeThienAn.Tests --no-restore
dotnet build HeThongBanLeThienAn.WinForms --no-restore
```

Kiểm thử hồi quy tự động vẫn dùng SQLite/InMemory tạm để chạy độc lập. Kiểm thử tích hợp SQL Server xác nhận migration, Stored Procedure, đăng nhập, phân trang, checkout và rollback/hủy đơn.

## Tác giả

Vương Nguyễn Trường Hưng — MSSV 2124110111 — Lớp CCQ2411D.

## SQL Server

- Runtime chỉ đăng ký `SqlServerSupermarketDbContext`; không còn lựa chọn provider hay file database nhúng.
- Toàn bộ migration chạy thật nằm trong `Migrations/SqlServer`.
- ADO.NET dùng `Microsoft.Data.SqlClient`, `SqlConnection`, `SqlCommand`, `SqlDataReader`, `SqlDataAdapter` và transaction thật trên SQL Server.
- SQLite chỉ còn là dependency của project kiểm thử để tạo database in-memory cô lập; không được đóng gói trong API.

Trong SSMS, kết nối Server name `localhost\\SQLEXPRESS`, chọn **Windows Authentication**, rồi mở database `MiniSupermarketDb`.
