# 🛒 HỆ THỐNG QUẢN LÝ SIÊU THỊ MINI (MINISUPERMARKET SYSTEM)

- **Môn học**: Lập trình Ứng dụng .NET Core (Mã môn: 229162)
- **Đồ án**: Phát triển Hệ thống Quản lý Bán lẻ và Theo dõi Hàng tồn kho theo thời gian thực

---

## 🏗️ 1. Mô hình Kiến trúc Hệ thống (Client - Server)

Dự án được xây dựng theo mô hình phân tầng hiện đại, tách biệt hoàn toàn giữa Backend và Frontend:
- **HeThongBanLeThienAn.API (Backend)**: Dự án ASP.NET Core Web API sử dụng Entity Framework Core Code-First (SQL Server), chịu trách nhiệm xử lý logic nghiệp vụ, xác thực JWT (JSON Web Token), phân quyền theo vai trò (Admin / Cashier) và cung cấp các RESTful API chuẩn hóa.
- **HeThongBanLeThienAn.WinForms (Frontend Client)**: Ứng dụng Windows Forms đóng vai trò là máy trạm POS tại quầy, sử dụng `HttpClient` đính kèm Bearer Token để giao tiếp với API, quản lý Danh mục, Sản phẩm và Khách hàng trực quan trên `DataGridView`.

---

## 🛠️ 2. Công nghệ Sử dụng

- **Ngôn ngữ**: C# (.NET 8.0)
- **Backend**: ASP.NET Core Web API, Entity Framework Core 8.0 (SQL Server), Controllers, JWT Bearer Authentication, LINQ Async/Await
- **Frontend**: Windows Forms (.NET 8.0), `System.Net.Http.Json`, `System.IdentityModel.Tokens.Jwt`
- **Công cụ phát triển & kiểm thử**: Visual Studio Code (VS Code), C# Dev Kit Extension, Postman, Swagger UI, xUnit Test Framework

---

## 📂 3. Cấu trúc Solution

```text
c:\HeThongBanLeThienAn
├── README.md
├── MiniSupermarket.postman_collection.json # Bộ sưu tập kiểm thử API trên Postman
├── .vscode/                             # Cấu hình Run & Debug cho VS Code
│   ├── launch.json                      # Cấu hình khởi chạy API, WinForms và Compound API + WinForms
│   └── tasks.json                       # Cấu hình Build và Stop process tự động
├── HeThongBanLeThienAn.sln
├── HeThongBanLeThienAn.API/             # Dự án Web API (Backend)
│   ├── Controllers/                     # AuthController, CategoriesController, ProductsController, CustomersController
│   ├── Data/                            # SupermarketDbContext & Data Seeding
│   ├── Models/                          # Category.cs, Product.cs, Customer.cs
│   └── Program.cs                       # Cấu hình EF Core SQLite, JWT Bearer, Swagger
├── HeThongBanLeThienAn.WinForms/        # Dự án Windows Forms (Frontend Client)
│   ├── FormLogin.cs                     # Đăng nhập lấy JWT Token & nạp thông tin phiên
│   ├── FormMainShell.cs                 # Khung điều khiển trung tâm (Sidebar + Dynamic Main Workspace)
│   ├── FormPOS.cs                       # Quầy bán hàng POS: quét barcode, giỏ hàng, thanh toán F9
│   ├── FormProductManagement.cs         # Quản lý sản phẩm, mã vạch, giá và tồn kho
│   ├── FormCategoryManagement.cs        # Quản lý danh mục & nhóm hàng
│   ├── FormCustomerManagement.cs        # Quản lý thông tin khách hàng & tích điểm
│   ├── FormQuickReport.cs               # Báo cáo doanh thu ca & hiệu suất (Admin)
│   ├── FormUserManagement.cs            # Quản trị tài khoản & phân quyền người dùng (Admin)
│   ├── NavigationManager.cs             # Bộ điều hướng phân quyền & quản lý form
│   └── SessionManager.cs                # Quản lý phiên làm việc & ApiClientService
└── HeThongBanLeThienAn.Tests/           # Dự án Unit Test (xUnit - 32 tests)
    ├── AuthControllerTests.cs           # Kiểm thử đăng nhập & xác thực JWT (Admin, Cashier, Warehouse)
    ├── CategoriesControllerTests.cs     # Kiểm thử API Danh mục CRUD & Phân quyền
    ├── ProductsControllerTests.cs       # Kiểm thử API Sản phẩm CRUD, Barcode & Tìm kiếm
    └── CustomersControllerTests.cs      # Kiểm thử API Khách hàng CRUD & Tìm kiếm
```

---

## 🔑 4. Danh sách 15 Tài khoản Kiểm thử Phân quyền (RBAC)

| STT | Username | Mật khẩu | Họ và Tên | Vai trò (Role) | Màn hình mặc định | Phạm vi quyền hạn |
| :---: | :--- | :---: | :--- | :---: | :---: | :--- |
| 1 | `admin01` | `123456` | Nguyễn Quản Trị | **Admin** | Quản lý Danh mục | Toàn quyền toàn bộ hệ thống |
| 2 | `admin02` | `123456` | Trần Giám Đốc | **Admin** | Quản lý Danh mục | Toàn quyền toàn bộ hệ thống |
| 3 | `cashier01` | `123456` | Lê Thu Ngân | **Cashier** | Quầy POS | Màn hình POS, Khách hàng thành viên |
| 4 | `cashier02` | `123456` | Phạm Bán Hàng | **Cashier** | Quầy POS | Màn hình POS, Khách hàng thành viên |
| 5 | `cashier03` | `123456` | Hoàng Thu Ngân | **Cashier** | Quầy POS | Màn hình POS, Khách hàng thành viên |
| 6 | `cashier04` | `123456` | Vũ Thị Quầy | **Cashier** | Quầy POS | Màn hình POS, Khách hàng thành viên |
| 7 | `cashier05` | `123456` | Đỗ Bán Lẻ | **Cashier** | Quầy POS | Màn hình POS, Khách hàng thành viên |
| 8 | `ware01` | `123456` | Ngô Quản Kho | **Warehouse** | Quản lý Sản phẩm | Quản lý Sản phẩm, Nhóm hàng tồn kho |
| 9 | `ware02` | `123456` | Bùi Kiểm Kê | **Warehouse** | Quản lý Sản phẩm | Quản lý Sản phẩm, Nhóm hàng tồn kho |
| 10 | `ware03` | `123456` | Dương Thủ Kho | **Warehouse** | Quản lý Sản phẩm | Quản lý Sản phẩm, Nhóm hàng tồn kho |
| 11 | `ware04` | `123456` | Lý Nhập Hàng | **Warehouse** | Quản lý Sản phẩm | Quản lý Sản phẩm, Nhóm hàng tồn kho |
| 12 | `admin_backup` | `123456` | Đặng Hỗ Trợ | **Admin** | Quản lý Danh mục | Toàn quyền toàn bộ hệ thống |
| 13 | `cashier06` | `123456` | Hồ Ca Chiều | **Cashier** | Quầy POS | Màn hình POS, Khách hàng thành viên |
| 14 | `ware05` | `123456` | Trương Vận Chuyển | **Warehouse** | Quản lý Sản phẩm | Quản lý Sản phẩm, Nhóm hàng tồn kho |
| 15 | `supervisor` | `123456` | Mai Giám Sát | **Admin** | Quản lý Danh mục | Toàn quyền toàn bộ hệ thống |

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

### Bước 2: Kiểm thử API bằng Postman 📬
1. Mở phần mềm **Postman**.
2. Chọn **Import** -> Tìm và chọn file `MiniSupermarket.postman_collection.json` ở thư mục gốc của dự án.
3. Thực hiện kiểm thử theo thứ tự:
   - **Xác thực (Auth)**: Chạy `Đăng nhập Admin` hoặc `Đăng nhập Cashier` (Postman sẽ tự động lưu `adminToken` / `cashierToken` vào biến môi trường).
   - **Quản lý Danh mục (Categories)**: Chạy các request Get All, Search, Create, Update, Delete.
   - **Quản lý Khách hàng (Customers)**: Chạy các request Get All, Search, Create, Update, Delete.

---

### Bước 3: Thử nghiệm ứng dụng WinForms
1. Nhập tài khoản:
   - `admin` / `123456` (Quyền Admin - toàn quyền CRUD)
   - `cashier` / `123456` (Quyền Cashier - xem, tìm kiếm, quản lý khách hàng)
2. Sau khi đăng nhập thành công, token JWT sẽ được lưu và mở giao diện `FormCategoryManagement` / `FormCustomerManagement`.
3. Thử nghiệm các tính năng: Tải lại dữ liệu, Thêm mới, Sửa, Xóa và Tìm kiếm theo từ khóa.

---

### Bước 4: Chạy Kiểm thử Đơn vị (Unit Tests)

Trong Terminal của VS Code, chạy lệnh sau:
```bash
dotnet test
```
Hoặc mở **Testing Tab** (Test Explorer) trong VS Code để xem và chạy toàn bộ 23 bài test tự động cho `AuthController`, `CategoriesController` và `CustomersController`.

---

## 👨‍💻 6. Tác giả

- **Họ tên sinh viên**: Vương Nguyễn Trường Hưng
- **Mã sinh viên**: 2124110111
- **Lớp học phần**: CCQ2411D
- **Đề tài**: Phát triển Hệ thống Quản lý Bán lẻ và Theo dõi Hàng tồn kho theo thời gian thực -- Cửa hàng Bách hóa Tổng hợp Thiên Ân
