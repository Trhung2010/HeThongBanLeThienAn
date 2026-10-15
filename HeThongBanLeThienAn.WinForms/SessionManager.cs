using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
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
            var loginObj = new { Username = username, Password = password };
            var response = await _rawClient.PostAsJsonAsync("auth/login", loginObj);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(jsonString);
                var root = doc.RootElement;

                SessionManager.JwtToken = root.TryGetProperty("token", out var pTok) ? pTok.GetString() ?? string.Empty : string.Empty;
                SessionManager.CurrentRole = root.TryGetProperty("role", out var pRole) ? pRole.GetString() ?? string.Empty : string.Empty;
                SessionManager.CurrentUsername = root.TryGetProperty("username", out var pUser) ? pUser.GetString() ?? username : username;
                SessionManager.CurrentFullName = root.TryGetProperty("fullName", out var pName) ? pName.GetString() ?? username : username;

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