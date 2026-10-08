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

        // 1. READ: Lấy toàn bộ danh sách nhóm hàng
        // GET /api/categories
        [HttpGet]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }

        // 2. READ: Lấy chi tiết một nhóm hàng theo ID
        // GET /api/categories/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> GetById(int id)
        {
            var cat = await _context.Categories.FindAsync(id);

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
        [Authorize(Roles = "Admin,Cashier")]
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
                .ToListAsync();

            return Ok(result);
        }

        // 4. CREATE: Thêm mới nhóm hàng
        // POST /api/categories
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] Category newCat)
        {
            if (string.IsNullOrWhiteSpace(newCat.CategoryName))
            {
                return BadRequest(new
                {
                    message = "Tên không được trống!"
                });
            }

            if (newCat.StockQuantity < 0)
            {
                return BadRequest(new
                {
                    message = "Số lượng tồn kho không được âm!"
                });
            }

            await _context.Categories.AddAsync(newCat);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = newCat.CategoryId },
                newCat);
        }

        // 5. UPDATE: Cập nhật thông tin nhóm hàng
        // PUT /api/categories/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] Category updateCat)
        {
            var cat = await _context.Categories.FindAsync(id);

            if (cat == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy nhóm hàng cần sửa!"
                });
            }

            if (updateCat.StockQuantity < 0)
            {
                return BadRequest(new
                {
                    message = "Số lượng tồn kho không được âm!"
                });
            }

            cat.CategoryName = updateCat.CategoryName;
            cat.Description = updateCat.Description;
            cat.StockQuantity = updateCat.StockQuantity;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 6. DELETE: Xóa nhóm hàng theo ID
        // DELETE /api/categories/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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
