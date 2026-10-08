# 🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI (MINISUPERMARKET SYSTEM)

- **Môn học**: Lập trình Ứng dụng .NET Core (Mã môn: 229162)
- **Buổi thực hành**: Buổi 1 - Xây dựng Web API quản lý danh mục và kết nối WinForms Client (CRUD)

---

## 🏗️ 1. Mô hình Kiến trúc Hệ thống (Client - Server)

Dự án được xây dựng theo mô hình phân tầng hiện đại, tách biệt hoàn toàn giữa Backend và Frontend:
- **HeThongBanLeThienAn.API (Backend)**: Dự án ASP.NET Core Web API chịu trách nhiệm xử lý logic nghiệp vụ, quản lý dữ liệu, xác thực JWT (JSON Web Token), phân quyền theo vai trò (Admin / Cashier) và cung cấp các RESTful API chuẩn hóa.
- **HeThongBanLeThienAn.WinForms (Frontend Client)**: Ứng dụng Windows Forms đóng vai trò là máy trạm POS tại quầy, sử dụng `HttpClient` để kết nối và truyền nhận Bearer Token với API qua mạng, quản lý danh mục và tồn kho trực quan trên `DataGridView`.

---

## 🛠️ 2. Công nghệ Sử dụng

- **Ngôn ngữ**: C# (.NET 8.0)
- **Backend**: ASP.NET Core Web API, Controllers, JWT Bearer Authentication, In-Memory Data, LINQ
- **Frontend**: Windows Forms (.NET 8.0), `System.Net.Http.Json`, `System.IdentityModel.Tokens.Jwt`
- **Công cụ kiểm thử & phát triển**: Visual Studio 2022, Swagger UI, xUnit Test Framework

---

## 📂 3. Cấu trúc Solution

```text
c:\HeThongBanLeThienAn
├── README.md
├── HeThongBanLeThienAn.sln
├── HeThongBanLeThienAn.API/             # Dự án Web API (Backend)
│   ├── Controllers/                     # Chứa AuthController, CategoriesController (JWT, CRUD & Search)
│   ├── Models/                          # Chứa lớp thực thể Category.cs
│   └── Program.cs                       # Cấu hình JWT Bearer, Swagger & Middleware
├── HeThongBanLeThienAn.WinForms/        # Dự án Windows Forms (Frontend Client)
│   ├── FormLogin.cs                     # Giao diện đăng nhập lấy JWT Token
│   ├── FormCategoryManagement.cs        # Giao diện quản lý danh mục & tồn kho
│   └── SessionManager.cs                # Quản lý phiên làm việc & JWT Token
└── HeThongBanLeThienAn.Tests/           # Dự án Unit Test (xUnit)
    ├── AuthControllerTests.cs           # Kiểm thử đăng nhập & xác thực JWT
    └── CategoriesControllerTests.cs     # Kiểm thử các API CRUD & Phân quyền
```

---

## 🔑 4. Tài khoản Mẫu & Phân quyền

| Tài khoản | Mật khẩu | Vai trò | Quyền hạn |
| --------- | -------- | ------- | --------- |
| `admin`   | `123456` | Admin   | Toàn quyền xem, thêm, sửa, xóa sản phẩm và truy cập Admin Dashboard |
| `cashier` | `123456` | Cashier | Quyền nhân viên: Xem danh sách, tìm kiếm sản phẩm và truy cập POS |

---

## 🚀 5. Hướng dẫn Chạy và Kiểm thử Dự án trong Visual Studio

### Bước 1: Mở Solution bằng Visual Studio
1. Mở Visual Studio 2022.
2. Chọn **Open a project or solution** và chọn mở file solution `HeThongBanLeThienAn.sln` (hoặc `HeThongBanLeThienAn.API/HeThongBanLeThienAn.sln`).

### Bước 2: Chạy phía Backend (Web API)
1. Nhấp chuột phải vào project **HeThongBanLeThienAn.API** chọn **Set as Startup Project**.
2. Nhấn **F5** (hoặc nút **Start**) để chạy.
3. Trình duyệt sẽ tự động mở giao diện Swagger UI (mặc định tại `http://localhost:5167/swagger`) để kiểm tra các phương thức GET, POST, PUT, DELETE với JWT Bearer Authentication.

### Bước 3: Chạy phía Frontend (WinForms Client)
1. Đảm bảo cổng (Port) của Web API đang chạy là `http://localhost:5167`.
2. Trong Visual Studio, nhấp chuột phải vào project **HeThongBanLeThienAn.WinForms** chọn **Debug** -> **Start new instance** (hoặc cấu hình Multiple Startup Projects trong Solution Properties).
3. Màn hình đăng nhập `FormLogin` hiển thị:
   - Nhập tài khoản `admin` / `123456` (Admin) hoặc `cashier` / `123456` (Cashier).
   - Đăng nhập thành công, token JWT được lưu và tự động mở `FormCategoryManagement`.
4. Thử nghiệm các chức năng: Tải danh sách, Thêm mới, Sửa, Xóa và Tìm kiếm nhóm hàng (Giao diện tự động phân quyền theo vai trò).

### Bước 4: Chạy Kiểm thử Đơn vị (Unit Tests)
1. Mở cửa sổ **Test Explorer** trong Visual Studio (`Test` -> `Test Explorer`).
2. Chọn **Run All Tests in View** (hoặc nhấn phím tắt `Ctrl+R, A`).
3. Kiểm tra kết quả 17 bài test tự động cho AuthController và CategoriesController đều vượt qua.

---

## 👨‍💻 6. Tác giả

- **Họ tên sinh viên**: Vương Nguyễn Trường Hưng
- **Mã sinh viên**: 2124110111
- **Lớp học phần**: CCQ2411D
- **Đề tài**: Phát triển Hệ thống Quản lý Bán lẻ và Theo dõi Hàng tồn kho theo thời gian thực -- Cửa hàng Bách hóa Tổng hợp Thiên Ân
