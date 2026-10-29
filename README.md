# Hệ thống Quản lý Bán lẻ Bách hóa Thiên Ân (MiniSupermarket)

Dự án này là một mẫu kiến trúc **Client - Server (3-Tier SOA)** sử dụng **ASP.NET Core Web API** và **Windows Forms (.NET 8)** để triển khai xác thực JWT không trạng thái, phân quyền dựa trên vai trò (RBAC), quản lý danh mục - sản phẩm phân trang máy chủ, quầy thu ngân POS thanh toán nguyên tử ACID và báo cáo điều hành trực quan.

## Tổng quan

- **Backend**: `HeThongBanLeThienAn.API`
- **Frontend**: `HeThongBanLeThienAn.WinForms`
- **Cơ sở dữ liệu**: Microsoft SQL Server Express (`localhost\SQLEXPRESS`, database `MiniSupermarketDb`)
- **Công nghệ**: .NET 8, ASP.NET Core Web API, JWT Bearer Authentication, Windows Forms (.NET 8), Entity Framework Core 8, ADO.NET (`Microsoft.Data.SqlClient`), GDI+
- **Chức năng chính**:
  - Đăng nhập xác thực tài khoản nhân sự với chuẩn băm mật khẩu an toàn **PBKDF2-SHA256** (120.000 vòng lặp, salt 16 bytes).
  - Cấp phát chuỗi **JSON Web Token (JWT)** chứa các Claims danh tính và vai trò.
  - Bảo vệ các endpoint API bằng thuộc tính `[Authorize]` và phân quyền theo vai trò `[Authorize(Roles = "...")]`.
  - Phân quyền 3 vai trò: **Admin** (Quản trị toàn diện), **Cashier** (Thu ngân bán hàng POS, khách hàng), **Warehouse** (Thủ kho quản lý hàng hóa, tồn kho).
  - Tái sử dụng kết nối mạng qua `ApiClientService` (HttpClient dùng chung) gửi Bearer Token từ WinForms đến API.
  - Quản lý danh mục 15 nhóm ngành hàng và 100 sản phẩm mã vạch, phân trang phía máy chủ bằng LINQ `.Skip().Take()`.
  - Quầy thu ngân **POS** bán hàng quét mã vạch, tích lũy điểm thành viên, đổi điểm chiết khấu tiền mặt, giao dịch nguyên tử **ACID** qua `DbTransaction` và cơ chế chống bán âm kho `AND StockQuantity >= @Qty`.
  - Quản lý khách hàng tự động xếp hạng thẻ (Đồng, Bạc, Vàng, Kim Cương), quản lý tài khoản nhân viên.
  - Báo cáo thống kê điều hành: 4 chỉ số KPI, tự động vẽ biểu đồ cột doanh thu bằng **GDI+** và Top 5 sản phẩm bán chạy qua Stored Procedures SQL Server.
  - Xuất dữ liệu DataGridView ra tệp CSV chuẩn UTF-8 BOM xem trực tiếp trên Microsoft Excel không bị lỗi font tiếng Việt.

## Kiến trúc hệ thống

```text
Người dùng (Nhân viên / Quản trị viên)
  │
  ▼
FormLogin (WinForms)
  │
  ├─ Gửi username + password đến API POST /api/auth/login
  │
  ▼
AuthController
  │
  ├─ Đối soát mật khẩu băm PBKDF2-SHA256 (PasswordSecurity.Verify)
  ├─ Tạo mã JWT token đóng gói Claims (Name, Role, FullName)
  └─ Trả về token + role
  │
  ▼
SessionManager & ApiClientService (WinForms)
  │
  ├─ Lưu JwtToken, CurrentRole, Username vào bộ nhớ phiên
  └─ Tự động gắn Header "Authorization: Bearer <token>" cho mọi request
  │
  ▼
FormMainShell (Single-Form Shell Workspace)
  │
  ├─ [Admin, Cashier]   ──► FormPOS (Bán hàng quét barcode, gọi POST /api/checkout)
  ├─ [Admin, Warehouse] ──► FormProductManagement (Gọi /api/products/paged, CRUD, xuất CSV)
  ├─ [Admin, Warehouse] ──► FormCategoryManagement (Gọi /api/categories, CRUD nhóm hàng)
  ├─ [Admin, Cashier]   ──► FormCustomerManagement (Gọi /api/customers/paged, tích & đổi điểm)
  ├─ [Admin]           ──► FormUserManagement (Gọi /api/users, phân quyền, khóa tài khoản)
  └─ [Admin]           ──► FormQuickReport (Gọi /api/reports, vẽ biểu đồ doanh thu GDI+)
  │
  ▼
Backend Web API Controllers (ASP.NET Core)
  │
  ├─ [Authorize] kiểm tra tính hợp lệ của JWT Bearer Token
  ├─ Cưỡng chế phân quyền RBAC theo vai trò
  │
  ▼
Data Access Layer
  ├─ EF Core 8 Code-First: Quản lý danh mục, phân trang LINQ, quan hệ thực thể
  └─ ADO.NET thuần (DbTransaction): Xử lý thanh toán POS nguyên tử ACID, Stored Procedures
  │
  ▼
Microsoft SQL Server Express (MiniSupermarketDb)
```

## Công nghệ sử dụng

- **Ngôn ngữ & Nền tảng**: C# 12 / .NET 8 (LTS)
- **Backend API**: ASP.NET Core Web API (.NET 8)
- **Bảo mật & Xác thực**:
  - `Microsoft.AspNetCore.Authentication.JwtBearer`
  - `System.IdentityModel.Tokens.Jwt`
  - Mật mã học an toàn: `Rfc2898DeriveBytes.Pbkdf2`, `RandomNumberGenerator`, `CryptographicOperations.FixedTimeEquals`
- **Truy xuất dữ liệu**:
  - `Microsoft.EntityFrameworkCore.SqlServer` (Code-First Migrations, Fluent API, Data Seeding)
  - `Microsoft.Data.SqlClient` (ADO.NET, `SqlConnection`, `SqlCommand`, `SqlDataReader`, `SqlTransaction`)
- **Frontend Client**:
  - Windows Forms (.NET 8)
  - Giao diện bàn làm việc đơn Single-Form Shell Workspace
  - `System.Drawing` (GDI+) vẽ biểu đồ thống kê doanh thu
  - `HttpClient` tập trung quản lý vòng đời socket
- **Kiểm thử & Tài liệu hóa**:
  - Swagger UI (`Swashbuckle.AspNetCore`)
  - Postman Collection (`MiniSupermarket.postman_collection.json`)

## Cấu trúc thư mục

```text
c:\HeThongBanLeThienAn
├── README.md
├── .gitignore
├── MiniSupermarket.postman_collection.json
├── HeThongBanLeThienAn.API/
│   ├── HeThongBanLeThienAn.sln
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── ProductsController.cs
│   │   ├── AdoProductController.cs
│   │   ├── CategoriesController.cs
│   │   ├── CustomersController.cs
│   │   ├── CheckoutController.cs
│   │   ├── OrdersController.cs
│   │   ├── ReportsController.cs
│   │   └── UsersController.cs
│   ├── Data/
│   │   ├── SupermarketDbContext.cs
│   │   ├── CheckoutService.cs
│   │   ├── UserDataStore.cs
│   │   └── StoredProcedures_Buoi5.sql
│   ├── Migrations/
│   │   └── SqlServer/
│   ├── Models/
│   │   ├── Product.cs
│   │   ├── Category.cs
│   │   ├── Customer.cs
│   │   ├── Order.cs
│   │   ├── User.cs
│   │   ├── OrderDetail.cs
│   │   ├── PagedResult.cs
│   │   ├── AdoDtos.cs
│   │   ├── ApiDtos.cs
│   │   └── DtoMappings.cs
│   ├── Security/
│   │   └── PasswordSecurity.cs
│   ├── Program.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   └── HeThongBanLeThienAn.API.csproj
├── HeThongBanLeThienAn.WinForms/
│   ├── FormMainShell.cs
│   ├── FormLogin.cs
│   ├── FormPOS.cs
│   ├── FormProductManagement.cs
│   ├── FormCategoryManagement.cs
│   ├── FormCustomerManagement.cs
│   ├── FormUserManagement.cs
│   ├── FormQuickReport.cs
│   ├── CsvExportService.cs
│   ├── ApiContracts.cs
│   ├── SessionManager.cs (chứa SessionManager và ApiClientService)
│   ├── Program.cs
│   └── HeThongBanLeThienAn.WinForms.csproj
└── HeThongBanLeThienAn.Tests/
    └── HeThongBanLeThienAn.Tests.csproj
```

> **Lưu ý:** Mã nguồn sử dụng namespace `MiniSupermarket.*`, trong khi thư mục project hiển thị là `HeThongBanLeThienAn.*`. Đây là sự quy ước tổ chức mã nguồn và namespace đã được thống nhất xuyên suốt dự án, không ảnh hưởng đến chức năng và kiến trúc hoạt động.

## Tài khoản mẫu

Hệ thống đã nạp sẵn 15 tài khoản nhân sự mẫu trong cơ sở dữ liệu SQL Server với mật khẩu được băm PBKDF2:

| Tài khoản | Mật khẩu mẫu | Vai trò (Role) | Phạm vi nghiệp vụ |
|---|---|---|---|
| `admin` | `admin@123` | **Admin** | Toàn quyền quản trị hệ thống, xem báo cáo KPI doanh thu, hủy đơn |
| `cashier01` | `cashier@123` | **Cashier** | Thu ngân quầy 01: bán hàng POS, tra cứu & tích điểm khách hàng |
| `warehouse01` | `wh@123` | **Warehouse** | Thủ kho: quản lý danh mục sản phẩm, nhóm hàng và tồn kho |

## API chính

### 1. Xác thực & Tài khoản (`/api/Auth`)
- `POST /api/auth/login`: Xác thực tài khoản nhân sự qua PBKDF2, cấp chuỗi JWT Bearer Token chứa Role.

### 2. Quản lý Sản phẩm (`/api/Products` & `/api/ado-products`)
- `GET /api/products`: Lấy toàn bộ 100 sản phẩm.
- `GET /api/products/paged?pageNumber=1&pageSize=5`: Phân trang sản phẩm máy chủ qua LINQ `.Skip().Take()`, tìm kiếm và lọc theo danh mục.
- `GET /api/products/{id}`: Xem chi tiết sản phẩm theo mã ID.
- `GET /api/products/barcode/{barcode}`: Tra cứu siêu tốc sản phẩm theo chuỗi mã vạch phục vụ thu ngân.
- `POST /api/products`: Thêm mới sản phẩm (`Admin, Warehouse`).
- `PUT /api/products/{id}`: Cập nhật giá, số lượng tồn kho (`Admin, Warehouse`).
- `DELETE /api/products/{id}`: Xóa sản phẩm (`Admin, Warehouse`).

### 3. Quản lý Nhóm ngành hàng (`/api/Categories`)
- `GET /api/categories`: Lấy danh sách 15 nhóm ngành hàng.
- `GET /api/categories/search?keyword=...`: Tìm kiếm nhóm ngành hàng.
- `GET /api/categories/staff-pos`: Danh mục phục vụ chọn nhanh tại quầy POS (`Admin, Cashier`).
- `GET /api/categories/admin-dashboard`: Thống kê sản phẩm theo danh mục (`Admin`).
- `POST /api/categories`: Thêm nhóm ngành hàng (`Admin, Warehouse`).
- `PUT /api/categories/{id}`: Cập nhật nhóm ngành hàng (`Admin, Warehouse`).
- `DELETE /api/categories/{id}`: Xóa nhóm ngành hàng (`Admin, Warehouse`).

### 4. Bán hàng POS & Đơn hàng (`/api/checkout` & `/api/Orders`)
- `POST /api/checkout`: Thực thi thanh toán POS trong `DbTransaction` nguyên tử ACID, trừ tồn kho chống bán âm, tích lũy điểm thưởng và hỗ trợ đổi điểm chiết khấu (`Admin, Cashier`).
- `DELETE /api/checkout/cancel/{orderId}`: Hủy đơn hàng, hoàn trả chính xác tồn kho và điểm thưởng trong transaction (`Admin`).
- `GET /api/orders`: Danh sách hóa đơn bán hàng.
- `GET /api/orders/paged`: Phân trang hóa đơn.

### 5. Khách hàng thành viên (`/api/Customers`)
- `GET /api/customers/paged`: Phân trang danh sách khách hàng theo điểm thưởng.
- `GET /api/customers/search?keyword=...`: Tìm kiếm khách hàng theo SĐT hoặc họ tên.
- `POST /api/customers`: Đăng ký thành viên mới (`Admin, Cashier`).
- `PUT /api/customers/{id}`: Cập nhật thông tin khách hàng (`Admin, Cashier`).

### 6. Báo cáo & Thống kê (`/api/Reports`)
- `GET /api/reports/dashboard-summary?fromDate=...&toDate=...`: Tổng hợp 4 chỉ số KPI, chuỗi doanh thu vẽ biểu đồ và Top 5 sản phẩm bán chạy (`Admin`).
- `GET /api/reports/revenue-by-cashier`: Báo cáo doanh thu theo từng thu ngân qua Stored Procedure (`Admin`).
- `GET /api/reports/revenue-by-date`: Báo cáo doanh thu theo ngày (`Admin`).
- `GET /api/reports/top-selling-products`: Danh sách sản phẩm bán chạy (`Admin`).

## Quy tắc phân quyền

Hệ thống áp dụng chính sách kiểm soát truy cập dựa trên vai trò (RBAC) nghiêm ngặt:
- `[Authorize(Roles = "Admin,Cashier,Warehouse")]`: Mọi nhân viên đã xác thực đều có quyền tra cứu cơ bản.
- `[Authorize(Roles = "Admin,Cashier")]`: Dành riêng cho phân hệ bán lẻ POS và chăm sóc khách hàng.
- `[Authorize(Roles = "Admin,Warehouse")]`: Dành cho phân hệ quản trị danh mục sản phẩm, nhóm hàng và tồn kho.
- `[Authorize(Roles = "Admin")]`: Chỉ Quản trị viên tối cao mới có quyền truy cập báo cáo tài chính KPI, hủy đơn hoàn tiền và quản lý tài khoản nhân viên.

Kết quả kiểm thử tiêu biểu:

| Endpoint | Quyền Admin | Quyền Cashier | Quyền Warehouse | Không có Token |
|---|:---:|:---:|:---:|:---:|
| `POST /api/checkout` | **200 OK** | **200 OK** | **403 Forbidden** | **401 Unauthorized** |
| `POST /api/products` | **201 Created** | **403 Forbidden** | **201 Created** | **401 Unauthorized** |
| `GET /api/reports/dashboard-summary` | **200 OK** | **403 Forbidden** | **403 Forbidden** | **401 Unauthorized** |
| `DELETE /api/checkout/cancel/{id}` | **200 OK** | **403 Forbidden** | **403 Forbidden** | **401 Unauthorized** |

## Hướng dẫn chạy project

### 1. Restore dependencies
```powershell
dotnet restore .\HeThongBanLeThienAn.API\HeThongBanLeThienAn.sln
```

### 2. Cập nhật Cơ sở dữ liệu SQL Server
Đảm bảo SQL Server Express đang chạy trên máy (`localhost\SQLEXPRESS`). Chạy lệnh migration:
```powershell
dotnet ef database update --context SqlServerSupermarketDbContext --project HeThongBanLeThienAn.API --startup-project HeThongBanLeThienAn.API
```

### 3. Chạy Backend Web API
```powershell
dotnet run --project .\HeThongBanLeThienAn.API\HeThongBanLeThienAn.API.csproj --urls "http://localhost:5167"
```
- API chạy tại: `http://localhost:5167`
- Swagger UI kiểm thử trực tiếp: `http://localhost:5167/swagger`

### 4. Chạy Frontend WinForms Client
Mở cửa sổ terminal mới và khởi chạy:
```powershell
dotnet run --project .\HeThongBanLeThienAn.WinForms\HeThongBanLeThienAn.WinForms.csproj
```
Ứng dụng WinForms sẽ mở màn hình đăng nhập `FormLogin`.

### 5. Chạy bộ kiểm thử xUnit
```powershell
dotnet test .\HeThongBanLeThienAn.Tests\HeThongBanLeThienAn.Tests.csproj
```

## Kiểm thử nhanh

### Không có token
Gọi:
```http
GET /api/categories
```
Kết quả mong đợi: `401 Unauthorized`.

### Đăng nhập thành công
Gửi:
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "admin@123"
}
```
Kết quả mong đợi: Nhận được chuỗi JWT Bearer Token, thời gian hết hạn và quyền `Admin`.

## Giao diện WinForms

1. Nhập tài khoản `admin` / `admin@123` (hoặc `cashier01` / `cashier@123`).
2. Hệ thống xác thực với Web API, lưu JWT vào `SessionManager`.
3. Mở giao diện bàn làm việc chính **`FormMainShell`**, tự động ẩn/hiện các menu chức năng tương ứng với quyền hạn của tài khoản:
   - Thu ngân: hiển thị màn hình **POS Bán hàng**, hỗ trợ quét mã vạch, thanh toán và in hóa đơn.
   - Thủ kho: hiển thị màn hình **Quản lý Sản phẩm**, hỗ trợ phân trang máy chủ, lọc nhóm hàng, tô đỏ tồn kho $\le 10$ và xuất CSV UTF-8.
   - Quản trị viên: toàn quyền truy cập phân hệ **Báo cáo Doanh thu** với biểu đồ GDI+ và quản lý người dùng.

## Lưu ý triển khai & Báo cáo đồ án

- Cơ sở dữ liệu hoạt động trực tiếp trên **Microsoft SQL Server Express** với đầy đủ ràng buộc khóa chính, khóa ngoại, chỉ mục duy nhất và 3 Stored Procedures.
- Khóa JWT trong `appsettings.json` chỉ là cấu hình demo. Khi triển khai thật phải chuyển sang User Secrets, biến môi trường hoặc kho bí mật và thay khóa mặc định.
- **Báo cáo đồ án**: Bản Word/PDF chính thức có mục lục, danh mục hình ảnh - bảng biểu, phân tích lý thuyết và kết quả kiểm thử; báo cáo được nộp riêng cho giảng viên, không lưu trong repository.

## Tác giả

- **Mã sinh viên:** 2124110111
- **Họ tên sinh viên:** Vương Nguyễn Trường Hưng
- **Lớp học phần:** CCQ2411D
- **Giảng viên hướng dẫn:** ThS. Nguyễn Cao Thái
- **Đề tài:** Phát triển Hệ thống Quản lý Bán lẻ và Theo dõi Hàng tồn kho theo thời gian thực -- Cửa hàng Bách hóa Tổng hợp Thiên Ân
