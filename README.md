# Hệ thống bán lẻ Thiên Ân

Ứng dụng thực hành .NET 8 gồm ASP.NET Core Web API, WinForms POS và SQLite.
Đã triển khai nội dung buổi 1–6: CRUD, JWT/phân quyền, EF Core migrations, ADO.NET transaction, phân trang LINQ và WinForms client-server.

## Nguồn dữ liệu

**Bộ dữ liệu tham khảo phục vụ thực hành, chưa xác minh từ website chính thức Bách Hóa Thiên Ân.**

- 100 mặt hàng, mã riêng biệt, 15 danh mục.
- 20 mặt hàng ban đầu giữ mã và giá mẫu. 80 mặt hàng thêm có mã nội bộ `TA-REF-0021` đến `TA-REF-0100`, tồn kho ban đầu 0; nhập tồn kho trước khi bán.
- Mã nội bộ không phải barcode nhà sản xuất; giá không phải bảng giá xác nhận của cửa hàng.
- `Products.DataSource` lưu nhãn nguồn tham khảo; mặt hàng nhập thủ công có nhãn riêng.
- CSDL mới không tự tạo khách mẫu. Hồ sơ khách được nhập qua API/WinForms. Migration chỉ dọn hồ sơ còn khớp chính xác tên và mã DEMO; giữ nguyên khách đã sửa và hóa đơn hiện có. Nhân viên và hóa đơn seed là dữ liệu thực hành.
- Hóa đơn mẫu có đủ 22 dòng chi tiết, tổng tiền và số lượng được đối soát. Số dư điểm seed là số dư thực hành, không phải sổ điểm của cửa hàng.

## Khởi chạy

```powershell
dotnet run --project HeThongBanLeThienAn.API
dotnet run --project HeThongBanLeThienAn.WinForms
```

API: http://localhost:5167; Swagger: http://localhost:5167/swagger.
SQLite nằm tại `HeThongBanLeThienAn.API/supermarket.db`, được giải đường dẫn theo thư mục ứng dụng.

API áp dụng migration lúc khởi động. **Không dùng EnsureDeleted/EnsureCreated hoặc kiểm tra số lượng bản ghi để reset CSDL.**
Trước khi cập nhật CSDL đang có, sao lưu bằng SQLite backup API (kể cả khi có WAL), không chỉ sao chép file đang ghi.
Migration `ReferenceCatalogAndSafeCheckout` nối tiếp migration ban đầu; không sửa lịch sử cũ.
Nếu CSDL cũ được tạo bằng EnsureCreated và chưa có migration history, cần đối soát/baseline riêng; ứng dụng sẽ báo lỗi thay vì tự xóa dữ liệu.

Tài khoản demo trong SQLite:

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
- SQLite không hỗ trợ stored procedure. `Data/StoredProcedures_Buoi5.sql` là tài liệu T-SQL tham khảo, không phải script đã chạy trên SQLite.

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

Kiểm thử hồi quy dùng SQLite tạm: migration mới/cũ, giữ tồn và giá đã sửa, thêm mặt hàng ngoài seed, chạy migration lại; đối soát hóa đơn; giá client giả; số lượng âm/0; dòng trùng; lỗi CSDL giữa giao dịch; khách vãng lai; hủy và hủy lặp; báo cáo theo ngày; đăng nhập và route checkout cũ.

## Tác giả

Vương Nguyễn Trường Hưng — MSSV 2124110111 — Lớp CCQ2411D.

## Hoàn tất bài 5 trên SQLite

`CompleteBuoi5Sqlite` là migration dữ liệu thật, chạy sau migration 100 mặt hàng.
Migration `RemoveDemoCustomers` có sẵn chỉ là bản rỗng; phần dọn có điều kiện nằm trong `CompleteBuoi5Sqlite`.
Đã giữ nguyên thay đổi menu phân quyền hiện có. Tiết 1 dùng `IDbConnectionFactory`/SQLite cho cả 3 phương thức ADO.NET.
SQLite không có stored procedure; các báo cáo tiết 3 sử dụng truy vấn SQL có tham số tương đương.

```powershell
dotnet ef database update --project HeThongBanLeThienAn.API
```

Sao lưu SQLite trước khi chạy. Migration dọn dữ liệu không hỗ trợ tự tái tạo hồ sơ mẫu bằng downgrade; dùng bản sao lưu nếu cần phục hồi.
