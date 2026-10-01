# Hệ thống quản lý siêu thị mini

Dự án này là một mẫu kiến trúc Client - Server sử dụng ASP.NET Core Web API và Windows Forms để triển khai xác thực JWT, phân quyền theo vai trò và quản lý danh mục hàng hóa.

## Tổng quan

- Backend: `HeThongBanLeThienAn.API`
- Frontend: `HeThongBanLeThienAn.WinForms`
- Công nghệ: .NET 8, ASP.NET Core, JWT Bearer Authentication, Windows Forms
- Chức năng chính:
  - Đăng nhập bằng tài khoản mẫu
  - Trả về JWT token
  - Bảo vệ các endpoint bằng `[Authorize]`
  - Phân quyền `Admin` và `Cashier`
  - Gửi `Bearer Token` từ WinForms đến API
  - Quản lý dữ liệu danh mục sản phẩm trong UI

---

## Kiến trúc hệ thống

```text
Người dùng
  │
  ▼
FormLogin (WinForms)
  │
  ├─ gửi username + password đến API /api/auth/login
  │
  ▼
AuthController
  │
  ├─ xác thực tài khoản mẫu
  ├─ tạo JWT token
  └─ trả về token + role
  │
  ▼
SessionManager
  │
  ├─ lưu JwtToken
  └─ lưu CurrentRole
  │
  ▼
FormCategoryManagement
  │
  └─ gọi API /api/categories với Authorization: Bearer <token>
  │
  ▼
CategoriesController
  │
  ├─ [Authorize]
  ├─ Admin: thêm, sửa, xóa, xem toàn bộ
  └─ Cashier: xem và tìm kiếm, không được sửa/xóa
```

---

## Công nghệ sử dụng

- C# / .NET 8
- ASP.NET Core Web API
- JWT Bearer Authentication
- Windows Forms (.NET 8)
- Swagger UI cho kiểm thử API
- `HttpClient` trong WinForms
- `System.IdentityModel.Tokens.Jwt`
- `Microsoft.AspNetCore.Authentication.JwtBearer`

---

## Cấu trúc thư mục

```text
c:\HeThongBanLeThienAn
├── README.md
├── HeThongBanLeThienAn.API/
│   ├── Controllers/
│   │   ├── AuthController.cs
│   │   ├── CategoriesController.cs
│   │   └── WeatherForecastController.cs
│   ├── Models/
│   │   └── Category.cs
│   ├── Program.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── HeThongBanLeThienAn.API.csproj
│   └── Properties/
│       └── launchSettings.json
├── HeThongBanLeThienAn.WinForms/
│   ├── FormLogin.cs
│   ├── FormLogin.Designer.cs
│   ├── FormCategoryManagement.cs
│   ├── FormCategoryManagement.Designer.cs
│   ├── SessionManager.cs
│   ├── Program.cs
│   ├── HeThongBanLeThienAn.WinForms.csproj
│   └── ...
└── HeThongBanLeThienAn.sln
```

> Lưu ý: mã nguồn hiện tại đang sử dụng namespace `MiniSupermarket.*`, trong khi folder project hiển thị là `HeThongBanLeThienAn.*`. Đây là tên project và namespace có thể không trùng hoàn toàn về mặt naming, nhưng chức năng và cấu trúc ứng dụng vẫn đúng như mô tả.

---

## Tài khoản mẫu

API hiện có hai tài khoản demo để kiểm thử:

| Tài khoản | Mật khẩu | Vai trò |
| --------- | -------- | ------- |
| `admin`   | `123456` | Admin   |
| `cashier` | `123456` | Cashier |

---

## API chính

### 1. Đăng nhập

Endpoint:

```http
POST /api/auth/login
```

Request body:

```json
{
  "username": "admin",
  "password": "123456"
}
```

Response thành công:

```json
{
  "success": true,
  "token": "<jwt_token>",
  "role": "Admin"
}
```

### 2. Lấy danh sách danh mục

Endpoint:

```http
GET /api/categories
```

- Yêu cầu xác thực JWT
- `Admin` và `Cashier` đều có quyền truy cập

### 3. Tìm kiếm danh mục

Endpoint:

```http
GET /api/categories/search?keyword=nuoc
```

- Yêu cầu xác thực JWT
- `Admin` và `Cashier` đều có quyền truy cập

### 4. Thêm danh mục

Endpoint:

```http
POST /api/categories
```

- Chỉ `Admin` được phép

### 5. Cập nhật danh mục

Endpoint:

```http
PUT /api/categories/{id}
```

- Chỉ `Admin` được phép

### 6. Xóa danh mục

Endpoint:

```http
DELETE /api/categories/{id}
```

- Chỉ `Admin` được phép

### 7. Kiểm tra phân quyền

```http
GET /api/categories/admin-dashboard
```

- Chỉ `Admin`

```http
GET /api/categories/staff-pos
```

- `Admin` và `Cashier` đều được phép

---

## Quy tắc phân quyền

```csharp
[Authorize(Roles = "Admin,Cashier")]
```

- `Admin` và `Cashier` đều có quyền xem danh mục

```csharp
[Authorize(Roles = "Admin")]
```

- Chỉ `Admin` mới có quyền thêm, sửa, xóa và truy cập trang quản trị

Kết quả kiểm thử tiêu biểu:

| Endpoint                              | Admin  | Cashier       |
| ------------------------------------- | ------ | ------------- |
| `GET /api/categories`                 | 200 OK | 200 OK        |
| `GET /api/categories/staff-pos`       | 200 OK | 200 OK        |
| `GET /api/categories/admin-dashboard` | 200 OK | 403 Forbidden |

---

## Hướng dẫn chạy project

### 1. Restore package

```powershell
dotnet restore
```

### 2. Chạy API

```powershell
dotnet run --project .\HeThongBanLeThienAn.API\HeThongBanLeThienAn.API.csproj
```

Hoặc mở project trong Visual Studio và chọn `HeThongBanLeThienAn.API` làm startup project.

API chạy mặc định tại:

```text
http://localhost:5167
```

Swagger UI:

```text
http://localhost:5167/swagger
```

### 3. Chạy WinForms

Mở project `HeThongBanLeThienAn.WinForms` trong Visual Studio và chạy ứng dụng.

WinForms sẽ mở màn hình đăng nhập `FormLogin`.

---

## Kiểm thử nhanh

### Không có token

Gọi:

```http
GET /api/categories
```

Kết quả mong đợi:

```text
401 Unauthorized
```

### Đăng nhập thành công

```http
POST /api/auth/login
```

Body:

```json
{
  "username": "admin",
  "password": "123456"
}
```

Kết quả mong đợi: nhận được JWT token và role `Admin`.

### Giao diện WinForms

- Nhập `admin` / `123456`
- Hệ thống lưu JWT vào `SessionManager`
- Mở `FormCategoryManagement`
- Gửi `Authorization: Bearer <token>` khi gọi API
- Danh mục sản phẩm sẽ tải lên `DataGridView`

---

## Lưu ý triển khai

- Dữ liệu hiện tại là dữ liệu mẫu, lưu trong bộ nhớ RAM, không có database.
- Secret key JWT đang được cấu hình trong `Program.cs` và `appsettings.json`.
- Đối với môi trường thực tế, nên thay thế bằng cơ sở dữ liệu và quản lý người dùng theo nghiệp vụ thực tế.

---

## Tác giả

- Mã sinh viên: 2124110111
- Họ tên sinh viên: Vương Nguyễn Trường Hưng
- Lớp học phần: CCQ2411D
- Đề tài: Phát triển Hệ thống Quản lý Bán lẻ và Theo dõi Hàng tồn kho theo thời gian thực -- Cửa hàng Bách hóa Tổng hợp Thiên Ân
