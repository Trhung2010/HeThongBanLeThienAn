using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System;
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
        private readonly SupermarketDbContext? _context;

        public AuthController(IConfiguration configuration, SupermarketDbContext? context = null)
        {
            _configuration = configuration;
            _context = context;
        }

        // Endpoint Đăng nhập: POST /api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { success = false, message = "Vui lòng nhập tài khoản và mật khẩu!" });
            }

            string inputUsername = request.Username.Trim();
            string inputPassword = request.Password.Trim();

            // 1. Kiểm tra trong CSDL (Bảng Users)
            if (_context != null)
            {
                var dbUser = _context.Users.FirstOrDefault(u =>
                    u.Username.ToLower() == inputUsername.ToLower());

                if (dbUser != null)
                {
                    if (!dbUser.IsActive)
                    {
                        return Unauthorized(new { success = false, message = "Tài khoản nhân viên này đã bị khóa!" });
                    }

                    if (dbUser.PasswordHash == inputPassword)
                    {
                        var token = GenerateJwtToken(dbUser.Username, dbUser.Role);
                        return Ok(new
                        {
                            success = true,
                            token = token,
                            role = dbUser.Role,
                            username = dbUser.Username,
                            fullName = dbUser.FullName
                        });
                    }
                }
            }

            if (_context != null)
                return Unauthorized(new { success = false, message = "Sai tài khoản hoặc mật khẩu!" });

            // Isolated teaching fixture used only when no database context is supplied.
            var user = UserDataStore.Users.FirstOrDefault(u =>
                u.Username.Equals(inputUsername, StringComparison.OrdinalIgnoreCase) &&
                u.Password == inputPassword &&
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
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
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