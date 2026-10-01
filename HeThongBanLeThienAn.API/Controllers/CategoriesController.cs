using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        // Dữ liệu mẫu lưu tạm trên bộ nhớ RAM
        private static readonly List<Category> _categories = new()
        {
            new Category
            {
                CategoryId = 1,
                CategoryName = "Bánh quy bơ",
                Description = "Sản phẩm bánh kẹo",
                StockQuantity = 120
            },
            new Category
            {
                CategoryId = 2,
                CategoryName = "Nước suối 500ml",
                Description = "Nước uống đóng chai",
                StockQuantity = 240
            },
            new Category
            {
                CategoryId = 3,
                CategoryName = "Sữa tươi hộp 1L",
                Description = "Sản phẩm từ sữa",
                StockQuantity = 85
            },
            new Category
            {
                CategoryId = 4,
                CategoryName = "Mì ăn liền",
                Description = "Thực phẩm đóng gói",
                StockQuantity = 160
            },
            new Category
            {
                CategoryId = 5,
                CategoryName = "Dầu ăn thực vật",
                Description = "Gia vị và dầu ăn",
                StockQuantity = 60
            }
        };

        // 1. READ: Lấy toàn bộ danh sách nhóm hàng
        // GET /api/categories
        [HttpGet]
        [Authorize(Roles = "Admin,Cashier")]
        public IActionResult GetAll()
        {
            return Ok(_categories);
        }

        // 2. READ: Lấy chi tiết một nhóm hàng theo ID
        // GET /api/categories/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Cashier")]
        public IActionResult GetById(int id)
        {
            var cat = _categories.FirstOrDefault(c => c.CategoryId == id);

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
        public IActionResult Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new
                {
                    message = "Vui lòng nhập từ khóa!"
                });
            }

            var result = _categories
                .Where(c => c.CategoryName.Contains(
                    keyword,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(result);
        }

        // 4. CREATE: Thêm mới nhóm hàng
        // POST /api/categories
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] Category newCat)
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

            newCat.CategoryId = _categories.Count > 0
                ? _categories.Max(c => c.CategoryId) + 1
                : 1;

            _categories.Add(newCat);

            return CreatedAtAction(
                nameof(GetById),
                new { id = newCat.CategoryId },
                newCat);
        }

        // 5. UPDATE: Cập nhật thông tin nhóm hàng
        // PUT /api/categories/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(
            int id,
            [FromBody] Category updateCat)
        {
            var cat = _categories.FirstOrDefault(
                c => c.CategoryId == id);

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

            return NoContent();
        }

        // 6. DELETE: Xóa nhóm hàng theo ID
        // DELETE /api/categories/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var cat = _categories.FirstOrDefault(
                c => c.CategoryId == id);

            if (cat == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy nhóm hàng cần xóa!"
                });
            }

            _categories.Remove(cat);

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
        // Admin và Cashier đều gọi được
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