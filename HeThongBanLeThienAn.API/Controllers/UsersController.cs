using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Security;

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

        // PHÂN TRANG PHÍA MÁY CHỦ: Phân trang danh sách người dùng (Chỉ Admin)
        // GET: /api/users/paged?keyword=...&pageNumber=1&pageSize=5
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged(
            [FromQuery] string? keyword,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            pageNumber = Math.Max(pageNumber, 1);
            pageSize = pageSize is >= 1 and <= 50 ? pageSize : 5;

            if (_context != null)
            {
                var query = _context.Users.AsNoTracking();

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    string search = keyword.Trim().ToLower();
                    query = query.Where(u => u.Username.ToLower().Contains(search) ||
                                             u.FullName.ToLower().Contains(search) ||
                                             u.Role.ToLower().Contains(search));
                }

                int totalItems = await query.CountAsync();
                int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
                int maxSafePage = (int.MaxValue / pageSize) + 1;
                pageNumber = Math.Min(pageNumber, maxSafePage);

                var items = await query
                    .OrderBy(u => u.UserId)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(u => new
                    {
                        Id = u.UserId,
                        Username = u.Username,
                        FullName = u.FullName,
                        Role = u.Role,
                        IsActive = u.IsActive
                    })
                    .ToListAsync();

                return Ok(new
                {
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    TotalItems = totalItems,
                    TotalPages = totalPages,
                    Items = items
                });
            }

            var memQuery = UserDataStore.Users.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim().ToLower();
                memQuery = memQuery.Where(u => u.Username.ToLower().Contains(search) ||
                                               u.FullName.ToLower().Contains(search) ||
                                               u.Role.ToLower().Contains(search));
            }

            int memTotal = memQuery.Count();
            int memPages = (int)Math.Ceiling(memTotal / (double)pageSize);
            var memItems = memQuery
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new
                {
                    Id = u.Id,
                    Username = u.Username,
                    FullName = u.FullName,
                    Role = u.Role,
                    IsActive = u.IsActive
                })
                .ToList();

            return Ok(new
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = memTotal,
                TotalPages = memPages,
                Items = memItems
            });
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

            string role = string.IsNullOrWhiteSpace(dto.Role) ? "Cashier" : dto.Role.Trim();
            string[] allowedRoles = ["Admin", "Cashier", "Warehouse"];
            role = allowedRoles.FirstOrDefault(r => r.Equals(role, StringComparison.OrdinalIgnoreCase)) ?? string.Empty;
            if (string.IsNullOrEmpty(role))
                return BadRequest(new { message = "Vai trò chỉ được phép là Admin, Cashier hoặc Warehouse!" });

            if (dto.Password.Length < 6)
                return BadRequest(new { message = "Mật khẩu phải có ít nhất 6 ký tự!" });

            string fullName = string.IsNullOrWhiteSpace(dto.FullName) ? username : dto.FullName.Trim();

            int createdId;
            if (_context != null)
            {
                var dbNewUser = new User
                {
                    Username = username,
                    PasswordHash = PasswordSecurity.Hash(dto.Password),
                    FullName = fullName,
                    Role = role,
                    IsActive = true
                };
                _context.Users.Add(dbNewUser);
                _context.SaveChanges();
                createdId = dbNewUser.UserId;
            }
            else
            {
                createdId = UserDataStore.Users.Any() ? UserDataStore.Users.Max(u => u.Id) + 1 : 1;
            }

            var newUser = new UserAccount
            {
                Id = createdId,
                Username = username,
                PasswordHash = PasswordSecurity.Hash(dto.Password),
                FullName = fullName,
                Role = role,
                IsActive = true
            };
            UserDataStore.Users.Add(newUser);

            return CreatedAtAction(nameof(GetAll), new { id = createdId }, new
            {
                Id = createdId,
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
