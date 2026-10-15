using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniSupermarket.API.Data;
using System;
using System.Linq;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        // GET: /api/users
        [HttpGet]
        public IActionResult GetAll()
        {
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

            if (UserDataStore.Users.Any(u => u.Username.Equals(dto.Username.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest(new { message = "Tên đăng nhập đã tồn tại trong hệ thống!" });
            }

            int nextId = UserDataStore.Users.Any() ? UserDataStore.Users.Max(u => u.Id) + 1 : 1;

            var newUser = new UserAccount
            {
                Id = nextId,
                Username = dto.Username.Trim(),
                Password = dto.Password.Trim(),
                FullName = string.IsNullOrWhiteSpace(dto.FullName) ? dto.Username.Trim() : dto.FullName.Trim(),
                Role = string.IsNullOrWhiteSpace(dto.Role) ? "Cashier" : dto.Role.Trim(),
                IsActive = true
            };

            UserDataStore.Users.Add(newUser);

            return CreatedAtAction(nameof(GetAll), new { id = newUser.Id }, new
            {
                Id = newUser.Id,
                Username = newUser.Username,
                FullName = newUser.FullName,
                Role = newUser.Role,
                IsActive = newUser.IsActive
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
