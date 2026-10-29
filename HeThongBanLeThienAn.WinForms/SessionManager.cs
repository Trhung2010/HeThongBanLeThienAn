using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MiniSupermarket.WinForms
{
    public static class SessionManager
    {
        public static string JwtToken { get; set; } = string.Empty;
        public static string CurrentRole { get; set; } = string.Empty;
        public static string CurrentUsername { get; set; } = string.Empty;
        public static string CurrentFullName { get; set; } = string.Empty;

        public static void Clear()
        {
            JwtToken = string.Empty;
            CurrentRole = string.Empty;
            CurrentUsername = string.Empty;
            CurrentFullName = string.Empty;
        }
    }

    public static class ApiClientService
    {
        private static readonly HttpClient _rawClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5167/api/")
        };

        // Thuộc tính Client tự động gắn Token Bearer của phiên hiện tại
        public static HttpClient Client
        {
            get
            {
                if (!string.IsNullOrEmpty(SessionManager.JwtToken))
                {
                    _rawClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
                }
                else
                {
                    _rawClient.DefaultRequestHeaders.Authorization = null;
                }
                return _rawClient;
            }
        }

        // Hàm gọi API đăng nhập lấy Token và lưu thông tin người dùng
        public static async Task<bool> LoginAsync(string username, string password)
        {
            var loginObj = new LoginRequestDto { Username = username, Password = password };
            var response = await _rawClient.PostAsJsonAsync("auth/login", loginObj);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                if (result == null || string.IsNullOrWhiteSpace(result.Token)) return false;

                SessionManager.JwtToken = result.Token;
                SessionManager.CurrentRole = result.Role;
                SessionManager.CurrentUsername = string.IsNullOrWhiteSpace(result.Username) ? username : result.Username;
                SessionManager.CurrentFullName = string.IsNullOrWhiteSpace(result.FullName) ? username : result.FullName;

                return true;
            }

            return false;
        }

        // Hàm gọi API lấy dữ liệu có gắn kèm Bearer Token bảo mật
        public static async Task<string> GetDataWithTokenAsync(string endpoint)
        {
            var response = await Client.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new Exception("Phiên làm việc hết hạn hoặc chưa đăng nhập!");
            }

            throw new Exception("Lỗi khi gọi dữ liệu từ Server.");
        }
    }
}
