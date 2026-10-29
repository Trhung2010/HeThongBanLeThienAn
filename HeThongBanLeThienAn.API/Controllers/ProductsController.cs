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

        private static bool IsNumericBarcode(string barcode) =>
            barcode.All(character => character is >= '0' and <= '9');

        // BUỔI 6: Phân trang phía máy chủ, tìm kiếm và lọc theo nhóm hàng.
        // GET /api/products/paged?keyword=Coca&categoryId=2&pageNumber=1&pageSize=5
        [HttpGet("paged")]
        [Authorize(Roles = "Admin,Cashier,Warehouse")]
        public async Task<ActionResult<PagedResult<ProductDto>>> GetPagedProducts(
            [FromQuery] string? keyword,
            [FromQuery] int? categoryId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            pageNumber = Math.Max(pageNumber, 1);
            pageSize = pageSize is >= 1 and <= 50 ? pageSize : 5;

            IQueryable<Product> query = _context.Products
                .Include(p => p.Category)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim().ToLower();
                query = query.Where(p =>
                    p.ProductName.ToLower().Contains(search) || p.Barcode.ToLower().Contains(search));
            }

            if (categoryId is > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            int maxSafePage = (int.MaxValue / pageSize) + 1;
            pageNumber = Math.Min(pageNumber, maxSafePage);

            var items = await query
                .OrderBy(p => p.ProductId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    Barcode = p.Barcode,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category == null ? string.Empty : p.Category.CategoryName,
                    DataSource = p.DataSource,
                    IsLowStock = p.StockQuantity <= 10
                })
                .ToListAsync();

            return Ok(new PagedResult<ProductDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            });
        }

        // 1. READ ALL: Lấy toàn bộ danh sách sản phẩm
        // GET /api/products
        [HttpGet]
        [Authorize(Roles = "Admin,Cashier,Warehouse")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products
                .AsNoTracking()
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    Barcode = p.Barcode,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    DataSource = p.DataSource,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId,
                    IsLowStock = p.StockQuantity <= 10
                })
                .ToListAsync();
            return Ok(products);
        }

        // BÀI TẬP 1 (MỞ RỘNG): Lấy danh sách sản phẩm kèm tên danh mục (Eager Loading)
        // GET /api/products/with-category
        [HttpGet("with-category")]
        [Authorize(Roles = "Admin,Cashier,Warehouse")]
        public async Task<IActionResult> GetWithCategory()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    Barcode = p.Barcode,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    IsLowStock = p.StockQuantity <= 10,
                    DataSource = p.DataSource,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null ? p.Category.CategoryName : string.Empty
                })
                .ToListAsync();

            return Ok(products);
        }

        // 2. READ BY ID: Lấy chi tiết một sản phẩm theo ID
        // GET /api/products/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Cashier,Warehouse")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Where(p => p.ProductId == id)
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    Barcode = p.Barcode,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    DataSource = p.DataSource,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId,
                    IsLowStock = p.StockQuantity <= 10
                })
                .SingleOrDefaultAsync();

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

            barcode = barcode.Trim();
            if (!IsNumericBarcode(barcode))
            {
                return BadRequest(new
                {
                    message = "Mã vạch chỉ được chứa chữ số từ 0 đến 9!"
                });
            }

            var product = await _context.Products
                .AsNoTracking()
                .Where(p => p.Barcode == barcode)
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    Barcode = p.Barcode,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    DataSource = p.DataSource,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId,
                    IsLowStock = p.StockQuantity <= 10
                })
                .SingleOrDefaultAsync();

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
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    Barcode = p.Barcode,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    DataSource = p.DataSource,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId,
                    IsLowStock = p.StockQuantity <= 10
                })
                .ToListAsync();

            return Ok(result);
        }

        // 5. CREATE: Thêm mới sản phẩm (Admin và Warehouse)
        // POST /api/products
        [HttpPost]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Create([FromBody] ProductUpsertDto request)
        {
            if (string.IsNullOrWhiteSpace(request.ProductName))
            {
                return BadRequest(new
                {
                    message = "Tên sản phẩm không được để trống!"
                });
            }

            if (string.IsNullOrWhiteSpace(request.Barcode))
            {
                return BadRequest(new
                {
                    message = "Mã vạch không được để trống!"
                });
            }

            request.Barcode = request.Barcode.Trim();
            if (!IsNumericBarcode(request.Barcode))
            {
                return BadRequest(new
                {
                    message = "Mã vạch chỉ được chứa chữ số từ 0 đến 9!"
                });
            }

            if (request.Price <= 0)
            {
                return BadRequest(new
                {
                    message = "Đơn giá phải lớn hơn 0!"
                });
            }

            if (request.StockQuantity < 0)
            {
                return BadRequest(new
                {
                    message = "Số lượng tồn kho không được âm!"
                });
            }

            var product = new Product
            {
                Barcode = request.Barcode,
                ProductName = request.ProductName.Trim(),
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                CategoryId = request.CategoryId,
                DataSource = "Nhập thủ công"
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = product.ProductId },
                product.ToDto());
        }

        // 6. UPDATE: Cập nhật thông tin sản phẩm (Admin và Warehouse)
        // PUT /api/products/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpsertDto request)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm cần sửa!"
                });
            }

            if (string.IsNullOrWhiteSpace(request.ProductName))
            {
                return BadRequest(new
                {
                    message = "Tên sản phẩm không được để trống!"
                });
            }

            if (string.IsNullOrWhiteSpace(request.Barcode))
            {
                return BadRequest(new
                {
                    message = "Mã vạch không được để trống!"
                });
            }

            request.Barcode = request.Barcode.Trim();
            if (!IsNumericBarcode(request.Barcode))
            {
                return BadRequest(new
                {
                    message = "Mã vạch chỉ được chứa chữ số từ 0 đến 9!"
                });
            }

            if (request.Price <= 0)
            {
                return BadRequest(new
                {
                    message = "Đơn giá phải lớn hơn 0!"
                });
            }

            if (request.StockQuantity < 0)
            {
                return BadRequest(new
                {
                    message = "Số lượng tồn kho không được âm!"
                });
            }

            product.ProductName = request.ProductName.Trim();
            product.Barcode = request.Barcode;
            product.Price = request.Price;
            product.StockQuantity = request.StockQuantity;
            if (request.CategoryId > 0)
            {
                product.CategoryId = request.CategoryId;
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
