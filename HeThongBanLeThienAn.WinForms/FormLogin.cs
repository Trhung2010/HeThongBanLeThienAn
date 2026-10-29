using System.Net.Http.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormLogin : Form
    {
        // Khởi tạo HttpClient để kết nối đến Web API
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5167/api/")
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
                var loginData = new LoginRequestDto
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
                    var loginResult = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                    if (loginResult == null || string.IsNullOrWhiteSpace(loginResult.Token))
                    {
                        MessageBox.Show("Phản hồi đăng nhập không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Lưu JWT Token vào SessionManager
                    SessionManager.JwtToken = loginResult.Token;

                    // Lưu quyền người dùng vào SessionManager
                    SessionManager.CurrentRole = loginResult.Role;

                    // Lưu username và họ tên vào SessionManager
                    SessionManager.CurrentUsername = string.IsNullOrWhiteSpace(loginResult.Username) ? username : loginResult.Username;
                    SessionManager.CurrentFullName = string.IsNullOrWhiteSpace(loginResult.FullName) ? username : loginResult.FullName;

                    // Hiển thị thông báo đăng nhập thành công
                    MessageBox.Show(
                        $"Đăng nhập thành công!\nXin chào: {SessionManager.CurrentFullName}\nVai trò: [{SessionManager.CurrentRole}]",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Mở FormMainShell - Khung điều khiển trung tâm
                    FormMainShell mainForm = new FormMainShell();
                    mainForm.StartPosition = FormStartPosition.CenterScreen;
                    Program.AppContext.MainForm = mainForm;
                    mainForm.Show();

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
