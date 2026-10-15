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
    public class ProductsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public ProductsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // 1. READ ALL: Lấy toàn bộ danh sách sản phẩm
        // GET /api/products
        [HttpGet]
        [Authorize(Roles = "Admin,Cashier,Warehouse")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        // 2. READ BY ID: Lấy chi tiết một sản phẩm theo ID
        // GET /api/products/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Cashier,Warehouse")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm!"
                });
            }

            return Ok(product);
        }

        // 3. READ BY BARCODE: Tìm sản phẩm theo mã vạch cho quầy POS
        // GET /api/products/barcode/{barcode}
        [HttpGet("barcode/{barcode}")]
        [Authorize(Roles = "Admin,Cashier,Warehouse")]
        public async Task<IActionResult> GetByBarcode(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
            {
                return BadRequest(new
                {
                    message = "Mã vạch không được để trống!"
                });
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Barcode == barcode.Trim());

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm có mã vạch này!"
                });
            }

            return Ok(product);
        }

        // 4. SEARCH: Tìm kiếm sản phẩm theo tên hoặc mã vạch
        // GET /api/products/search?keyword=...
        [HttpGet("search")]
        [Authorize(Roles = "Admin,Cashier,Warehouse")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new
                {
                    message = "Vui lòng nhập từ khóa tìm kiếm!"
                });
            }

            var result = await _context.Products
                .Where(p => p.ProductName.Contains(keyword) || p.Barcode.Contains(keyword))
                .ToListAsync();

            return Ok(result);
        }

        // 5. CREATE: Thêm mới sản phẩm (Admin và Warehouse)
        // POST /api/products
        [HttpPost]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (string.IsNullOrWhiteSpace(product.ProductName))
            {
                return BadRequest(new
                {
                    message = "Tên sản phẩm không được để trống!"
                });
            }

            if (string.IsNullOrWhiteSpace(product.Barcode))
            {
                return BadRequest(new
                {
                    message = "Mã vạch không được để trống!"
                });
            }

            if (product.Price < 0)
            {
                return BadRequest(new
                {
                    message = "Đơn giá không được âm!"
                });
            }

            if (product.StockQuantity < 0)
            {
                return BadRequest(new
                {
                    message = "Số lượng tồn kho không được âm!"
                });
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = product.ProductId },
                product);
        }

        // 6. UPDATE: Cập nhật thông tin sản phẩm (Admin và Warehouse)
        // PUT /api/products/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Update(int id, [FromBody] Product updateData)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm cần sửa!"
                });
            }

            if (string.IsNullOrWhiteSpace(updateData.ProductName))
            {
                return BadRequest(new
                {
                    message = "Tên sản phẩm không được để trống!"
                });
            }

            if (string.IsNullOrWhiteSpace(updateData.Barcode))
            {
                return BadRequest(new
                {
                    message = "Mã vạch không được để trống!"
                });
            }

            if (updateData.Price < 0)
            {
                return BadRequest(new
                {
                    message = "Đơn giá không được âm!"
                });
            }

            if (updateData.StockQuantity < 0)
            {
                return BadRequest(new
                {
                    message = "Số lượng tồn kho không được âm!"
                });
            }

            product.ProductName = updateData.ProductName.Trim();
            product.Barcode = updateData.Barcode.Trim();
            product.Price = updateData.Price;
            product.StockQuantity = updateData.StockQuantity;
            if (updateData.CategoryId > 0)
            {
                product.CategoryId = updateData.CategoryId;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 7. DELETE: Xóa sản phẩm theo ID (Admin và Warehouse)
        // DELETE /api/products/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm cần xóa!"
                });
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
