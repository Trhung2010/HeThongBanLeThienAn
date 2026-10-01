# 🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI (MINISUPERMARKET SYSTEM)

> **Môn học:** Lập trình Ứng dụng .NET Core (Mã môn: 229162)  
> **Buổi thực hành:** Buổi 2 - Xác thực JWT, Phân quyền và tích hợp WinForms Client

---

## 🏗️ 1. Mô hình Kiến trúc Hệ thống (Client - Server)

Dự án tiếp tục sử dụng mô hình Client - Server, gồm Backend Web API và Frontend WinForms:

- **`MiniSupermarket.API` (Backend):** Dự án ASP.NET Core Web API chịu trách nhiệm xử lý xác thực JWT, phân quyền người dùng và cung cấp các RESTful API được bảo vệ bằng `[Authorize]`.
- **`MiniSupermarket.WinForms` (Frontend Client):** Ứng dụng Windows Forms cung cấp giao diện đăng nhập, lưu JWT Token và gọi API kèm `Bearer Token` để truy cập các chức năng được bảo vệ.

Luồng hoạt động chính:

```text
Người dùng
    │
    ▼
FormLogin (WinForms)
    │  Username + Password
    ▼
AuthController (/auth/login)
    │
    │  JWT Token + Role
    ▼
SessionManager
    │
    │  Bearer Token
    ▼
FormCategoryManagement
    │
    ▼
CategoriesController
    │
    ├── [Authorize]
    ├── Admin Dashboard
    └── Staff POS
```

---

## 🛠️ 2. Công nghệ Sử dụng

- **Ngôn ngữ:** C# (.NET 8.0)
- **Backend:** ASP.NET Core Web API, JWT Bearer Authentication, Controllers, `[Authorize]`, Role-based Authorization
- **Frontend:** Windows Forms (.NET 8.0), `HttpClient`, `System.Net.Http.Json`
- **Xác thực:** JSON Web Token (JWT)
- **Quản lý phiên:** `SessionManager`
- **Công cụ kiểm thử:** Swagger UI
- **NuGet:** `System.IdentityModel.Tokens.Jwt`, `Microsoft.AspNetCore.Authentication.JwtBearer`

---

## 🔐 3. Các chức năng chính của Buổi 2

### 3.1. Đăng nhập và cấp JWT Token

`AuthController` cung cấp API:

```text
POST /api/auth/login
```

Tài khoản kiểm thử:

| Tài khoản | Mật khẩu | Quyền   |
| --------- | -------- | ------- |
| `admin`   | `123456` | Admin   |
| `cashier` | `123456` | Cashier |

Khi đăng nhập thành công, API trả về:

- `success`
- `token`
- `role`

JWT Token được lưu vào `SessionManager`.

---

### 3.2. Bảo vệ API bằng `[Authorize]`

`CategoriesController` được bảo vệ bằng:

```csharp
[Authorize]
```

Do đó người dùng phải đăng nhập và gửi JWT Token hợp lệ mới có thể truy cập các API được bảo vệ.

Nếu gọi API không có Token:

```text
401 Unauthorized
```

---

### 3.3. Phân quyền Admin và Cashier

Hệ thống có hai API kiểm thử phân quyền:

```text
GET /api/categories/admin-dashboard
GET /api/categories/staff-pos
```

Phân quyền:

```csharp
[Authorize(Roles = "Admin")]
```

`admin-dashboard` chỉ cho phép tài khoản có quyền `Admin`.

```csharp
[Authorize(Roles = "Admin,Cashier")]
```

`staff-pos` cho phép cả `Admin` và `Cashier`.

Kết quả kiểm thử:

| API                           |  Admin |       Cashier |
| ----------------------------- | -----: | ------------: |
| `/categories/staff-pos`       | 200 OK |        200 OK |
| `/categories/admin-dashboard` | 200 OK | 403 Forbidden |

---

## 🖥️ 4. WinForms Client

### 4.1. FormLogin

`FormLogin` là màn hình đăng nhập của ứng dụng WinForms.

Các thành phần chính:

- `txtUser`: nhập tài khoản.
- `txtPass`: nhập mật khẩu.
- `btnLogin`: nút đăng nhập hệ thống.
- `SessionManager`: lưu JWT Token và quyền hiện tại.

Sau khi đăng nhập thành công:

```text
FormLogin
    ↓
Lưu JwtToken + CurrentRole
    ↓
Mở FormCategoryManagement
```

---

### 4.2. SessionManager

`SessionManager` lưu thông tin phiên đăng nhập:

```csharp
public static class SessionManager
{
    public static string JwtToken { get; set; } = string.Empty;
    public static string CurrentRole { get; set; } = string.Empty;
}
```

---

### 4.3. Gửi Bearer Token khi gọi API

Trong `FormCategoryManagement`, `HttpClient` được tạo và gắn JWT Token:

```csharp
client.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue(
        "Bearer",
        SessionManager.JwtToken);
```

Sau đó WinForms gọi API:

```text
GET /api/categories
```

với JWT Token trong HTTP Authorization Header.

---

## 📂 5. Cấu trúc Solution

```text
HeThongBanLeThienAnSystem/
│
├── HeThongBanLeThienAn.API/             # Dự án Web API (Backend)
│   ├── Controllers/
│   │   ├── AuthController.cs            # Đăng nhập và cấp JWT
│   │   └── CategoriesController.cs      # API danh mục + phân quyền
│   ├── Models/                          # Các lớp dữ liệu
│   └── Program.cs                       # Cấu hình JWT Authentication
│
└── HeThongBanLeThienAn.WinForms/       # Dự án Windows Forms
    ├── FormLogin.cs                     # Giao diện đăng nhập
    ├── FormLogin.Designer.cs            # Thiết kế FormLogin
    ├── FormCategoryManagement.cs        # Quản lý danh mục
    ├── SessionManager.cs                # Lưu JWT Token và Role
    └── Program.cs                       # Khởi động FormLogin
```

---

## 🚀 6. Hướng dẫn Chạy và Kiểm thử Dự án

### Bước 1: Chạy Backend Web API

Mở Solution bằng Visual Studio.

Đảm bảo project:

```text
HeThongBanLeThienAn.API
```

được chạy cùng với WinForms.

API sử dụng địa chỉ:

```text
https://localhost:7163
```

Swagger:

```text
https://localhost:7163/swagger/index.html
```

---

### Bước 2: Kiểm thử API không có Token

Mở Swagger và gọi:

```text
GET /api/categories
```

khi chưa Authorize.

Kết quả mong đợi:

```text
401 Unauthorized
```

---

### Bước 3: Kiểm thử đăng nhập trên Swagger

Gọi:

```text
POST /api/auth/login
```

với:

```json
{
  "username": "admin",
  "password": "123456"
}
```

Hoặc:

```json
{
  "username": "cashier",
  "password": "123456"
}
```

API trả về JWT Token và Role.

---

### Bước 4: Kiểm thử phân quyền

Trong Swagger chọn **Authorize** và nhập:

```text
Bearer <JWT_TOKEN>
```

Sau đó kiểm tra:

```text
GET /api/categories/staff-pos
GET /api/categories/admin-dashboard
```

Đối với tài khoản `cashier`:

```text
staff-pos           → 200 OK
admin-dashboard     → 403 Forbidden
```

---

### Bước 5: Chạy WinForms

Chạy:

```text
MiniSupermarket.WinForms
```

Ứng dụng phải mở:

```text
FormLogin
```

Đăng nhập bằng:

```text
Tài khoản: admin
Mật khẩu: 123456
```

hoặc:

```text
Tài khoản: cashier
Mật khẩu: 123456
```

Sau khi đăng nhập thành công, hệ thống lưu JWT Token và mở:

```text
FormCategoryManagement
```

---

### Bước 6: Kiểm thử gọi API từ WinForms

Sau khi đăng nhập thành công, `FormCategoryManagement` gọi:

```text
GET /api/categories
```

và gửi:

```text
Authorization: Bearer <JWT_TOKEN>
```

Nếu Token hợp lệ, danh sách nhóm hàng được tải lên `DataGridView`.

---

## 🧪 7. Kết quả Kiểm thử

Các trường hợp cần kiểm chứng:

| STT | Nội dung kiểm thử                     | Kết quả mong đợi            |
| --: | ------------------------------------- | --------------------------- |
|   1 | Truy cập `/categories` không có Token | `401 Unauthorized`          |
|   2 | Đăng nhập `admin / 123456`            | Đăng nhập thành công        |
|   3 | Đăng nhập `cashier / 123456`          | Đăng nhập thành công        |
|   4 | Sai tài khoản/mật khẩu                | Đăng nhập thất bại          |
|   5 | Admin gọi `staff-pos`                 | `200 OK`                    |
|   6 | Cashier gọi `staff-pos`               | `200 OK`                    |
|   7 | Admin gọi `admin-dashboard`           | `200 OK`                    |
|   8 | Cashier gọi `admin-dashboard`         | `403 Forbidden`             |
|   9 | WinForms đăng nhập thành công         | Mở `FormCategoryManagement` |
|  10 | WinForms gọi `/categories` có JWT     | Hiển thị danh sách danh mục |

---

## 📸 8. Ảnh Minh chứng

Có thể chụp các màn hình sau để đưa vào báo cáo:

1. Swagger API `POST /auth/login`.
2. Kết quả trả về JWT Token.
3. `GET /api/categories` không có Token → `401`.
4. Swagger Authorize bằng JWT Token.
5. `staff-pos` → `200 OK`.
6. `admin-dashboard` với Cashier → `403 Forbidden`.
7. FormLogin của WinForms.
8. Thông báo đăng nhập thành công.
9. FormCategoryManagement hiển thị danh sách nhóm hàng sau khi đăng nhập.

---

## 👨‍💻 9. Tác giả

**Họ tên sinh viên:** [Nguyễn Vũ Trường Sơn]

**Mã sinh viên:** [2124110109]

**Lớp học phần:** [CCQ2411D]
