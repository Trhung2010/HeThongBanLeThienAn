using System.Net.Http.Json;
using System.Text.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormLogin : Form
    {
        // Khởi tạo HttpClient để kết nối đến Web API
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7163/api/")
        };

        // Hàm khởi tạo FormLogin
        public FormLogin()
        {
            InitializeComponent();
        }

        // Xử lý sự kiện click nút đăng nhập
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();

            // Kiểm tra tài khoản và mật khẩu có bị bỏ trống không
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ tài khoản và mật khẩu!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                // Tạo dữ liệu đăng nhập gửi đến API
                var loginData = new
                {
                    Username = username,
                    Password = password
                };

                // Gửi yêu cầu đăng nhập đến AuthController
                var response = await _client.PostAsJsonAsync(
                    "auth/login",
                    loginData);

                // Kiểm tra đăng nhập thành công
                if (response.IsSuccessStatusCode)
                {
                    // Đọc dữ liệu JSON trả về từ Server
                    var jsonString =
                        await response.Content.ReadAsStringAsync();

                    using var doc = JsonDocument.Parse(jsonString);

                    // Lưu JWT Token vào SessionManager
                    SessionManager.JwtToken =
                        doc.RootElement
                            .GetProperty("token")
                            .GetString() ?? string.Empty;

                    // Lưu quyền người dùng vào SessionManager
                    SessionManager.CurrentRole =
                        doc.RootElement
                            .GetProperty("role")
                            .GetString() ?? string.Empty;

                    // Hiển thị thông báo đăng nhập thành công
                    MessageBox.Show(
                        $"Đăng nhập thành công với quyền:\n{SessionManager.CurrentRole}",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Mở Form quản lý danh mục
                    FormCategoryManagement mainForm =
                        new FormCategoryManagement();

                    this.Hide();

                    mainForm.ShowDialog();

                    this.Close();
                }
                else
                {
                    // Thông báo khi đăng nhập thất bại
                    MessageBox.Show(
                        "Sai tài khoản hoặc mật khẩu!",
                        "Đăng nhập thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi kết nối đến Server
                MessageBox.Show(
                    "Lỗi kết nối đến Server: " + ex.Message,
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}