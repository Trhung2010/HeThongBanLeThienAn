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

        // 1. READ ALL: Lấy toàn bộ danh sách khách hàng
        // GET /api/customers
        [HttpGet]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        // 2. READ BY ID: Lấy chi tiết một khách hàng theo ID
        // GET /api/customers/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

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
                .ToListAsync();

            return Ok(result);
        }

        // 4. CREATE: Thêm mới khách hàng
        // POST /api/customers
        [HttpPost]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.CustomerName))
            {
                return BadRequest(new
                {
                    message = "Tên khách hàng không được để trống!"
                });
            }

            if (string.IsNullOrWhiteSpace(customer.PhoneNumber))
            {
                return BadRequest(new
                {
                    message = "Số điện thoại không được để trống!"
                });
            }

            if (customer.RewardPoints < 0)
            {
                return BadRequest(new
                {
                    message = "Điểm thưởng không được âm!"
                });
            }

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = customer.CustomerId },
                customer);
        }

        // 5. UPDATE: Cập nhật thông tin khách hàng
        // PUT /api/customers/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> Update(int id, [FromBody] Customer updateData)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng cần sửa!"
                });
            }

            if (string.IsNullOrWhiteSpace(updateData.CustomerName))
            {
                return BadRequest(new
                {
                    message = "Tên khách hàng không được để trống!"
                });
            }

            if (string.IsNullOrWhiteSpace(updateData.PhoneNumber))
            {
                return BadRequest(new
                {
                    message = "Số điện thoại không được để trống!"
                });
            }

            if (updateData.RewardPoints < 0)
            {
                return BadRequest(new
                {
                    message = "Điểm thưởng không được âm!"
                });
            }

            customer.CustomerName = updateData.CustomerName.Trim();
            customer.PhoneNumber = updateData.PhoneNumber.Trim();
            customer.Address = updateData.Address?.Trim();
            customer.RewardPoints = updateData.RewardPoints;
            customer.MembershipRank = string.IsNullOrWhiteSpace(updateData.MembershipRank) ? "Chuẩn" : updateData.MembershipRank.Trim();

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
