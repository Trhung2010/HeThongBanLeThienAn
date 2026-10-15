using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
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

        // GET /api/orders
        [HttpGet]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .OrderByDescending(o => o.CreatedDate)
                .ToListAsync();

            return Ok(orders);
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
            int totalItems = 0;

            // 1. Trừ số lượng tồn kho sản phẩm
            foreach (var item in request.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    product.StockQuantity = Math.Max(0, product.StockQuantity - item.Quantity);
                    totalAmount += item.UnitPrice * item.Quantity;
                    totalItems += item.Quantity;
                }
            }

            // 2. Tích điểm cho khách hàng thân thiết nếu có SĐT
            string customerInfo = "Khách vãng lai";
            Customer? customer = null;
            int pointsEarned = (int)(totalAmount / 10000m);

            if (!string.IsNullOrWhiteSpace(request.CustomerPhone))
            {
                string cleanPhone = request.CustomerPhone.Trim();
                customer = await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == cleanPhone);
                if (customer != null)
                {
                    customer.RewardPoints += pointsEarned;

                    // Tự động nâng hạng thẻ
                    if (customer.RewardPoints >= 1000)
                    {
                        customer.MembershipRank = "Bạch Kim";
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

            string orderCode = "HD-" + DateTime.Now.ToString("yyyyMMddHHmmss");
            int customerId = customer?.CustomerId ?? 1;

            var newOrder = new Order
            {
                OrderCode = orderCode,
                CreatedDate = DateTime.Now,
                CustomerId = customerId,
                CashierUsername = string.IsNullOrWhiteSpace(request.CashierUsername) ? "cashier01" : request.CashierUsername,
                TotalItems = totalItems,
                FinalAmount = totalAmount,
                RewardPoints = customer != null ? pointsEarned : 0
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            foreach (var item in request.Items)
            {
                _context.OrderDetails.Add(new OrderDetail
                {
                    OrderId = newOrder.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    LineTotal = item.Quantity * item.UnitPrice
                });
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Thanh toán thành công và đã xuất hóa đơn!",
                orderCode = orderCode,
                cashier = newOrder.CashierUsername,
                totalAmount = totalAmount,
                customer = customerInfo,
                timestamp = newOrder.CreatedDate
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
