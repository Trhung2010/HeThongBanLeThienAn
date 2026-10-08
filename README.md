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
- **Công cụ phát triển & kiểm thử**: Visual Studio Code (VS Code), C# Dev Kit Extension, Swagger UI, xUnit Test Framework

---

## 📂 3. Cấu trúc Solution

```text
c:\HeThongBanLeThienAn
├── README.md
├── .vscode/                             # Cấu hình Run & Debug cho VS Code
│   ├── launch.json                      # Cấu hình khởi chạy API, WinForms và Compound API + WinForms
│   ├── tasks.json                       # Cấu hình Build và Stop process tự động
│   └── settings.json
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

## 🚀 5. Hướng dẫn Chạy và Kiểm thử Dự án trong Visual Studio Code (VS Code)

### Bước 1: Mở dự án trong VS Code
1. Mở Visual Studio Code.
2. Chọn **File** -> **Open Folder...** và chọn thư mục gốc dự án (`HeThongBanLeThienAn`).
3. Khuyến nghị cài đặt Extension **C#** hoặc **C# Dev Kit** từ Microsoft.

---

### Cách 1: Chạy trực tiếp bằng Run & Debug trong VS Code (Khuyến nghị)
1. Bấm tổ hợp phím **`Ctrl + Shift + D`** (hoặc chọn biểu tượng **Run and Debug** trên thanh bên trái).
2. Tại menu thả xuống phía trên, chọn cấu hình: **`API + WinForms`** (Compound run cả Backend và Frontend cùng lúc).
3. Nhấn **F5** hoặc nút **Play**:
   - VS Code sẽ tự động build và khởi chạy **Web API** (`http://localhost:5167`).
   - Màn hình Swagger UI sẽ sẵn sàng tại `http://localhost:5167/swagger`.
   - Ứng dụng **WinForms Client** (`FormLogin`) sẽ tự động mở lên.

---

### Cách 2: Chạy qua Terminal bằng .NET CLI

#### 1. Restore các package
Mở Terminal trong VS Code (**`Ctrl + ~`**) và chạy:
```bash
dotnet restore
```

#### 2. Chạy Backend Web API
Mở Terminal 1 và chạy:
```bash
dotnet run --project ./HeThongBanLeThienAn.API/HeThongBanLeThienAn.API.csproj
```
> Web API sẽ lắng nghe tại `http://localhost:5167`. Bạn có thể truy cập Swagger UI qua trình duyệt: `http://localhost:5167/swagger`.

#### 3. Chạy Frontend WinForms Client
Mở Terminal 2 và chạy:
```bash
dotnet run --project ./HeThongBanLeThienAn.WinForms/HeThongBanLeThienAn.WinForms.csproj
```

---

### Bước 2: Thử nghiệm ứng dụng WinForms
1. Nhập tài khoản:
   - `admin` / `123456` (Quyền Admin - toàn quyền CRUD)
   - `cashier` / `123456` (Quyền Cashier - chỉ xem và tìm kiếm)
2. Sau khi đăng nhập thành công, token JWT sẽ được lưu và mở giao diện `FormCategoryManagement`.
3. Thử nghiệm các tính năng: Tải lại dữ liệu, Thêm sản phẩm, Sửa sản phẩm, Xóa sản phẩm, Tìm kiếm theo từ khóa.

---

### Bước 3: Chạy Kiểm thử Đơn vị (Unit Tests)

Trong Terminal của VS Code, chạy lệnh sau:
```bash
dotnet test
```
Hoặc mở phím tắt trong VS Code **Testing Tab** (Test Explorer) để xem và chạy toàn bộ 17 bài test tự động cho `AuthController` và `CategoriesController`.

---

## 👨‍💻 6. Tác giả

- **Họ tên sinh viên**: Vương Nguyễn Trường Hưng
- **Mã sinh viên**: 2124110111
- **Lớp học phần**: CCQ2411D
- **Đề tài**: Phát triển Hệ thống Quản lý Bán lẻ và Theo dõi Hàng tồn kho theo thời gian thực -- Cửa hàng Bách hóa Tổng hợp Thiên Ân
