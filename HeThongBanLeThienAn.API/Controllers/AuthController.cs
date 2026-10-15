using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MiniSupermarket.API.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Endpoint Đăng nhập: POST /api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { success = false, message = "Vui lòng nhập tài khoản và mật khẩu!" });
            }

            // Tra cứu tài khoản từ UserDataStore (Hỗ trợ 15 tài khoản kiểm thử Buổi 4)
            var user = UserDataStore.Users.FirstOrDefault(u =>
                u.Username.Equals(request.Username.Trim(), StringComparison.OrdinalIgnoreCase) &&
                u.Password == request.Password.Trim() &&
                u.IsActive);

            if (user != null)
            {
                var token = GenerateJwtToken(user.Username, user.Role);
                return Ok(new
                {
                    success = true,
                    token = token,
                    role = user.Role,
                    username = user.Username,
                    fullName = user.FullName
                });
            }

            return Unauthorized(new { success = false, message = "Sai tài khoản hoặc mật khẩu!" });
        }

        private string GenerateJwtToken(string username, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"] ??
                "SupermarketSecretKeyDoAnMonHoc2026SecureString!!");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginRequestDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}