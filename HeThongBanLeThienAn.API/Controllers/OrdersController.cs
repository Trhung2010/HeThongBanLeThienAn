using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public OrdersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // POST /api/orders/checkout
        [HttpPost("checkout")]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequestDto request)
        {
            if (request == null || request.Items == null || !request.Items.Any())
            {
                return BadRequest(new { success = false, message = "Giỏ hàng rỗng, không thể thanh toán!" });
            }

            decimal totalAmount = 0;

            // 1. Trừ số lượng tồn kho sản phẩm
            foreach (var item in request.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    product.StockQuantity = Math.Max(0, product.StockQuantity - item.Quantity);
                    totalAmount += item.UnitPrice * item.Quantity;
                }
            }

            // 2. Tích điểm cho khách hàng thân thiết nếu có SĐT
            string customerInfo = "Khách vãng lai";
            if (!string.IsNullOrWhiteSpace(request.CustomerPhone))
            {
                string cleanPhone = request.CustomerPhone.Trim();
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == cleanPhone);
                if (customer != null)
                {
                    int pointsEarned = (int)(totalAmount / 10000m);
                    customer.RewardPoints += pointsEarned;

                    // Tự động nâng hạng thẻ
                    if (customer.RewardPoints >= 1000)
                    {
                        customer.MembershipRank = "Kim Cương";
                    }
                    else if (customer.RewardPoints >= 500)
                    {
                        customer.MembershipRank = "Vàng";
                    }
                    else if (customer.RewardPoints >= 200)
                    {
                        customer.MembershipRank = "Bạc";
                    }

                    customerInfo = $"{customer.CustomerName} ({customer.MembershipRank}) - +{pointsEarned} điểm";
                }
            }

            await _context.SaveChangesAsync();

            string orderCode = "HD-" + DateTime.Now.ToString("yyyyMMddHHmmss");

            return Ok(new
            {
                success = true,
                message = "Thanh toán thành công và đã xuất hóa đơn!",
                orderCode = orderCode,
                cashier = request.CashierUsername,
                totalAmount = totalAmount,
                customer = customerInfo,
                timestamp = DateTime.Now
            });
        }
    }

    public class CheckoutRequestDto
    {
        public string CashierUsername { get; set; } = string.Empty;
        public string? CustomerPhone { get; set; }
        public List<CheckoutItemDto> Items { get; set; } = new();
    }

    public class CheckoutItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
