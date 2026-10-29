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
    public class CustomersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public CustomersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // Bài tập mở rộng Buổi 6: khách hàng có điểm cao hiển thị trước.
        // GET /api/customers/paged?keyword=...&pageNumber=1&pageSize=5
        [HttpGet("paged")]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<ActionResult<PagedResult<CustomerDto>>> GetPaged(
            [FromQuery] string? keyword,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            pageNumber = Math.Max(pageNumber, 1);
            pageSize = pageSize is >= 1 and <= 50 ? pageSize : 5;

            var query = _context.Customers.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim().ToLower();
                query = query.Where(c => c.CustomerName.ToLower().Contains(search) ||
                                         c.PhoneNumber.ToLower().Contains(search) ||
                                         c.MembershipRank.ToLower().Contains(search) ||
                                         (c.Address != null && c.Address.ToLower().Contains(search)));
            }

            int totalItems = await query.CountAsync();
            int maxSafePage = (int.MaxValue / pageSize) + 1;
            pageNumber = Math.Min(pageNumber, maxSafePage);

            var items = await query
                .OrderByDescending(c => c.RewardPoints)
                .ThenBy(c => c.CustomerId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    RewardPoints = c.RewardPoints,
                    MembershipRank = c.MembershipRank,
                    CreatedDate = c.CreatedDate
                })
                .ToListAsync();

            return Ok(new PagedResult<CustomerDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = items
            });
        }

        // 1. READ ALL: Lấy toàn bộ danh sách khách hàng
        // GET /api/customers
        [HttpGet]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _context.Customers
                .AsNoTracking()
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    RewardPoints = c.RewardPoints,
                    MembershipRank = c.MembershipRank,
                    CreatedDate = c.CreatedDate
                })
                .ToListAsync();
            return Ok(customers);
        }

        // 2. READ BY ID: Lấy chi tiết một khách hàng theo ID
        // GET /api/customers/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers
                .AsNoTracking()
                .Where(c => c.CustomerId == id)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    RewardPoints = c.RewardPoints,
                    MembershipRank = c.MembershipRank,
                    CreatedDate = c.CreatedDate
                })
                .SingleOrDefaultAsync();

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng!"
                });
            }

            return Ok(customer);
        }

        // 3. SEARCH: Tìm kiếm khách hàng theo tên, sđt hoặc hạng thẻ
        // GET /api/customers/search?keyword=...
        [HttpGet("search")]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new
                {
                    message = "Vui lòng nhập từ khóa tìm kiếm!"
                });
            }

            var result = await _context.Customers
                .Where(c => c.CustomerName.Contains(keyword) ||
                            c.PhoneNumber.Contains(keyword) ||
                            c.MembershipRank.Contains(keyword))
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    RewardPoints = c.RewardPoints,
                    MembershipRank = c.MembershipRank,
                    CreatedDate = c.CreatedDate
                })
                .ToListAsync();

            return Ok(result);
        }

        // 4. CREATE: Thêm mới khách hàng
        // POST /api/customers
        [HttpPost]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> Create([FromBody] CustomerUpsertDto request)
        {
            if (string.IsNullOrWhiteSpace(request.CustomerName))
            {
                return BadRequest(new
                {
                    message = "Tên khách hàng không được để trống!"
                });
            }

            if (string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                return BadRequest(new
                {
                    message = "Số điện thoại không được để trống!"
                });
            }

            if (request.RewardPoints < 0)
            {
                return BadRequest(new
                {
                    message = "Điểm thưởng không được âm!"
                });
            }

            var customer = new Customer
            {
                CustomerName = request.CustomerName.Trim(),
                PhoneNumber = request.PhoneNumber.Trim(),
                Address = request.Address?.Trim(),
                RewardPoints = request.RewardPoints,
                MembershipRank = string.IsNullOrWhiteSpace(request.MembershipRank) ? "Chuẩn" : request.MembershipRank.Trim(),
                CreatedDate = DateTime.Now
            };

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = customer.CustomerId },
                customer.ToDto());
        }

        // 5. UPDATE: Cập nhật thông tin khách hàng
        // PUT /api/customers/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerUpsertDto request)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng cần sửa!"
                });
            }

            if (string.IsNullOrWhiteSpace(request.CustomerName))
            {
                return BadRequest(new
                {
                    message = "Tên khách hàng không được để trống!"
                });
            }

            if (string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                return BadRequest(new
                {
                    message = "Số điện thoại không được để trống!"
                });
            }

            if (request.RewardPoints < 0)
            {
                return BadRequest(new
                {
                    message = "Điểm thưởng không được âm!"
                });
            }

            customer.CustomerName = request.CustomerName.Trim();
            customer.PhoneNumber = request.PhoneNumber.Trim();
            customer.Address = request.Address?.Trim();
            customer.RewardPoints = request.RewardPoints;
            customer.MembershipRank = string.IsNullOrWhiteSpace(request.MembershipRank) ? "Chuẩn" : request.MembershipRank.Trim();

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 6. DELETE: Xóa khách hàng theo ID
        // DELETE /api/customers/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng cần xóa!"
                });
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
