using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System;
using System.Linq;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly SupermarketDbContext? _context;

        public UsersController(SupermarketDbContext? context = null)
        {
            _context = context;
        }

        // GET: /api/users
        [HttpGet]
        public IActionResult GetAll()
        {
            if (_context != null)
            {
                var dbUserDtos = _context.Users.Select(u => new
                {
                    Id = u.UserId,
                    Username = u.Username,
                    FullName = u.FullName,
                    Role = u.Role,
                    IsActive = u.IsActive
                }).ToList();

                return Ok(dbUserDtos);
            }

            var userDtos = UserDataStore.Users.Select(u => new
            {
                Id = u.Id,
                Username = u.Username,
                FullName = u.FullName,
                Role = u.Role,
                IsActive = u.IsActive
            }).ToList();

            return Ok(userDtos);
        }

        // POST: /api/users
        [HttpPost]
        public IActionResult Create([FromBody] CreateUserDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest(new { message = "Tên đăng nhập và mật khẩu không được để trống!" });
            }

            string username = dto.Username.Trim();
            if ((_context != null && _context.Users.Any(u => u.Username.ToLower() == username.ToLower())) ||
                UserDataStore.Users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest(new { message = "Tên đăng nhập đã tồn tại trong hệ thống!" });
            }

            int nextId = (_context != null && _context.Users.Any())
                ? _context.Users.Max(u => u.UserId) + 1
                : (UserDataStore.Users.Any() ? UserDataStore.Users.Max(u => u.Id) + 1 : 1);

            string role = string.IsNullOrWhiteSpace(dto.Role) ? "Cashier" : dto.Role.Trim();
            string fullName = string.IsNullOrWhiteSpace(dto.FullName) ? username : dto.FullName.Trim();

            if (_context != null)
            {
                var dbNewUser = new User
                {
                    UserId = nextId,
                    Username = username,
                    PasswordHash = dto.Password.Trim(),
                    FullName = fullName,
                    Role = role,
                    IsActive = true
                };
                _context.Users.Add(dbNewUser);
                _context.SaveChanges();
            }

            var newUser = new UserAccount
            {
                Id = nextId,
                Username = username,
                Password = dto.Password.Trim(),
                FullName = fullName,
                Role = role,
                IsActive = true
            };
            UserDataStore.Users.Add(newUser);

            return CreatedAtAction(nameof(GetAll), new { id = nextId }, new
            {
                Id = nextId,
                Username = username,
                FullName = fullName,
                Role = role,
                IsActive = true
            });
        }
    }

    public class CreateUserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = "Cashier";
    }
}
