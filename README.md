# Hệ thống Bán lẻ Bách hóa Thiên Ân (MiniSupermarket)

Dự án đồ án học phần **Lập trình ứng dụng C# .NET Core** (Buổi 1 – Buổi 6).  
Hệ thống quản trị bán lẻ theo kiến trúc phân tầng chuyên nghiệp: **ASP.NET Core Web API (Backend)** kết hợp **Windows Forms Client (Desktop)** và cơ sở dữ liệu **Microsoft SQL Server Express**.

---

## 1. Thành phần kiến trúc hệ thống

- **Backend Web API (`HeThongBanLeThienAn.API`)**:
  - Cung cấp **33 Endpoints RESTful** phục vụ toàn bộ nghiệp vụ bán hàng, quản lý và báo cáo điều hành.
  - Tích hợp tài liệu tương tác trực quan qua **Swagger UI** tại `http://localhost:5167/swagger`.
  - Bảo mật không trạng thái bằng **JWT Bearer Token** kết hợp cơ chế kiểm soát truy cập phân quyền dựa trên vai trò (**Role-Based Access Control - RBAC**).
  - Thuật toán băm mật khẩu chuẩn bảo mật OWASP: **PBKDF2-SHA256** với muối ngẫu nhiên 16 byte (128-bit) và 120.000 vòng lặp, chống tấn công vét cạn bằng GPU và so sánh chuỗi thời gian không đổi chống Timing Attack.
  - Quản lý dữ liệu hỗn hợp: **Entity Framework Core 8 Code-First Migrations** cho danh mục, phân trang LINQ server-side (`.Skip()`, `.Take()`) kết hợp **ADO.NET thuần (`Microsoft.Data.SqlClient`)** cho giao dịch thanh toán POS đạt chuẩn **ACID** trong `DbTransaction`.
  - Cơ chế cập nhật kho hàng nguyên tử có điều kiện `AND StockQuantity >= @Qty` triệt tiêu hoàn toàn lỗi bán âm kho khi nhiều quầy thanh toán đồng thời.

- **Frontend Client (`HeThongBanLeThienAn.WinForms`)**:
  - Thiết kế theo kiến trúc bàn làm việc đơn thống nhất **Single-Form Shell Workspace (`FormMainShell`)**.
  - Giao diện quầy thu ngân **POS (`FormPOS`)**: bắt sự kiện phím tắt, quét mã vạch USB HID, tính chiết khấu điểm thưởng thành viên, thanh toán nguyên tử và in hóa đơn.
  - Quản lý danh mục hàng hóa **(`FormProductManagement`)**: DataGridView phân trang máy chủ, lọc nhóm ngành hàng, tìm kiếm thời gian thực, cảnh báo tồn kho thấp ($\le 10$) bằng màu sắc và xuất dữ liệu ra file CSV chuẩn UTF-8 BOM xem trực tiếp trên Excel không lỗi font.
  - Quản lý khách hàng thành viên **(`FormCustomerManagement`)**: tra cứu số điện thoại, tích lũy điểm thưởng và phân hạng thẻ tự động (Đồng, Bạc, Vàng, Kim Cương).
  - Quản lý tài khoản nhân viên **(`FormUserManagement`)**: phân quyền RBAC (Admin, Cashier, Warehouse), quản lý kích hoạt / khóa tài khoản.
  - Báo cáo thống kê điều hành **(`FormQuickReport`)**: tổng hợp 4 chỉ số KPI kinh doanh, tự động vẽ biểu đồ cột doanh thu bằng **GDI+** và bảng thống kê Top 5 sản phẩm bán chạy qua các Stored Procedure SQL Server.

---

## 2. Nguồn dữ liệu & Tài khoản kiểm thử

Hệ thống được nạp sẵn bộ dữ liệu danh mục thực hành phong phú thông qua cơ chế Data Seeding:
- **100 mặt hàng** với mã vạch số 12 chữ số liên tiếp từ `893456789001` đến `893456789100`.
- **15 nhóm ngành hàng** siêu thị phân loại chi tiết.
- **15 tài khoản nhân viên** phân quyền mẫu trong SQL Server:

| Tên tài khoản | Vai trò (Role) | Mật khẩu mặc định | Quyền hạn và phạm vi nghiệp vụ |
| :--- | :---: | :---: | :--- |
| `admin` | **Admin** | `admin@123` | Toàn quyền quản trị hệ thống, xem báo cáo KPI doanh thu, hủy đơn |
| `cashier01` | **Cashier** | `cashier@123` | Thu ngân quầy 01, thực thi bán hàng POS và quản lý khách hàng |
| `warehouse01` | **Warehouse** | `wh@123` | Thủ kho, quản lý danh mục sản phẩm, nhóm hàng và kiểm kê tồn kho |

---

## 3. Hướng dẫn khởi chạy hệ thống

### 3.1. Yêu cầu môi trường
- .NET 8.0 SDK (hoặc mới hơn).
- Microsoft SQL Server 2019/2022 hoặc SQL Server Express (`localhost\SQLEXPRESS`).

### 3.2. Cập nhật Cơ sở dữ liệu (Database Migration)
Cấu hình chuỗi kết nối trong tệp `HeThongBanLeThienAn.API/appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=MiniSupermarketDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```
Khởi chạy migration để tự động tạo cấu trúc bảng, chỉ mục Unique, Stored Procedure và seed dữ liệu mẫu:
```powershell
dotnet ef database update --context SqlServerSupermarketDbContext --project HeThongBanLeThienAn.API --startup-project HeThongBanLeThienAn.API
```

### 3.3. Chạy ứng dụng
Mở 2 cửa sổ terminal độc lập:

1. **Khởi động Backend Web API**:
   ```powershell
   dotnet run --project HeThongBanLeThienAn.API --urls "http://localhost:5167"
   ```
   Truy cập tài liệu Swagger tại: `http://localhost:5167/swagger`

2. **Khởi động Frontend WinForms Client**:
   ```powershell
   dotnet run --project HeThongBanLeThienAn.WinForms
   ```

---

## 4. Báo cáo Đồ án

> **Lưu ý:**
> Quyển **Báo cáo Đồ án tốt nghiệp chính thức** (định dạng Microsoft Word `.docx` và Adobe PDF `.pdf` đạt chuẩn **80 trang**, đầy đủ mục lục, danh mục bảng biểu, sơ đồ kiến trúc và phân tích học thuật chi tiết) **được nộp riêng trực tiếp cho Giảng viên hướng dẫn / Hội đồng nghiệm thu**, không lưu trữ trực tiếp trên repository GitHub này.

---

## 5. Thông tin tác giả

- **Sinh viên thực hiện:** Vương Nguyễn Trường Hưng
- **Mã số sinh viên (MSSV):** 2124110111
- **Lớp học phần:** CCQ2411D — Lập trình ứng dụng C# .NET Core
- **Giảng viên giảng dạy:** ThS. Nguyễn Cao Thái
- **Thời gian hoàn thành:** Học kỳ I, năm học 2026 - 2027
