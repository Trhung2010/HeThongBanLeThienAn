using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public CategoriesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // PHÂN TRANG PHÍA MÁY CHỦ: Phân trang danh mục nhóm hàng
        // GET /api/categories/paged?keyword=...&pageNumber=1&pageSize=5
        [HttpGet("paged")]
        [Authorize(Roles = "Admin,Cashier,Warehouse")]
        public async Task<ActionResult<PagedResult<CategoryDto>>> GetPaged(
            [FromQuery] string? keyword,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            pageNumber = Math.Max(pageNumber, 1);
            pageSize = pageSize is >= 1 and <= 50 ? pageSize : 5;

            var query = _context.Categories.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim().ToLower();
                query = query.Where(c => c.CategoryName.ToLower().Contains(search) ||
                                         (c.Description != null && c.Description.ToLower().Contains(search)));
            }

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            int maxSafePage = (int.MaxValue / pageSize) + 1;
            pageNumber = Math.Min(pageNumber, maxSafePage);

            var items = await query
                .OrderBy(c => c.CategoryId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Description = c.Description
                })
                .ToListAsync();

            return Ok(new PagedResult<CategoryDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            });
        }

        // 1. READ: Lấy toàn bộ danh sách nhóm hàng
        // GET /api/categories
        [HttpGet]
        [Authorize(Roles = "Admin,Cashier,Warehouse")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _context.Categories
                .AsNoTracking()
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Description = c.Description
                })
                .ToListAsync();
            return Ok(categories);
        }

        // 2. READ: Lấy chi tiết một nhóm hàng theo ID
        // GET /api/categories/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Cashier,Warehouse")]
        public async Task<IActionResult> GetById(int id)
        {
            var cat = await _context.Categories
                .AsNoTracking()
                .Where(c => c.CategoryId == id)
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Description = c.Description
                })
                .SingleOrDefaultAsync();

            if (cat == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy nhóm hàng!"
                });
            }

            return Ok(cat);
        }

        // 3. SEARCH: Tìm kiếm nhóm hàng
        // GET /api/categories/search?keyword=...
        [HttpGet("search")]
        [Authorize(Roles = "Admin,Cashier,Warehouse")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new
                {
                    message = "Vui lòng nhập từ khóa!"
                });
            }

            var result = await _context.Categories
                .Where(c => c.CategoryName.Contains(keyword))
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Description = c.Description
                })
                .ToListAsync();

            return Ok(result);
        }

        // 4. CREATE: Thêm mới nhóm hàng
        // POST /api/categories
        [HttpPost]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Create([FromBody] CategoryUpsertDto request)
        {
            if (string.IsNullOrWhiteSpace(request.CategoryName))
            {
                return BadRequest(new
                {
                    message = "Tên không được trống!"
                });
            }

            var category = new Category
            {
                CategoryName = request.CategoryName.Trim(),
                Description = request.Description?.Trim()
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = category.CategoryId },
                category.ToDto());
        }

        // 5. UPDATE: Cập nhật thông tin nhóm hàng
        // PUT /api/categories/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryUpsertDto request)
        {
            var cat = await _context.Categories.FindAsync(id);

            if (cat == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy nhóm hàng cần sửa!"
                });
            }

            if (string.IsNullOrWhiteSpace(request.CategoryName))
            {
                return BadRequest(new { message = "Tên không được trống!" });
            }

            cat.CategoryName = request.CategoryName.Trim();
            cat.Description = request.Description?.Trim();

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 6. DELETE: Xóa nhóm hàng theo ID
        // DELETE /api/categories/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Delete(int id)
        {
            var cat = await _context.Categories.FindAsync(id);

            if (cat == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy nhóm hàng cần xóa!"
                });
            }

            _context.Categories.Remove(cat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 7. Kiểm tra quyền Admin
        [HttpGet("admin-dashboard")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAdminDashboard()
        {
            return Ok(new
            {
                message = "Chào mừng Admin! Bạn có toàn quyền quản trị hệ thống siêu thị mini."
            });
        }

        // 8. Kiểm tra quyền chung cho nhân viên
        [HttpGet("staff-pos")]
        [Authorize(Roles = "Admin,Cashier")]
        public IActionResult GetStaffPos()
        {
            return Ok(new
            {
                message = "Màn hình POS Thu ngân sẵn sàng phục vụ bán hàng."
            });
        }
    }
}
