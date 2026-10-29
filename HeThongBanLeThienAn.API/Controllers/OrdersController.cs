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

        private readonly CheckoutService _checkout;

        public OrdersController(SupermarketDbContext context, CheckoutService checkout)
        {
            _context = context;
            _checkout = checkout;
        }

        // PHÂN TRANG PHÍA MÁY CHỦ: Phân trang danh sách đơn hàng
        // GET /api/orders/paged?keyword=...&pageNumber=1&pageSize=5
        [HttpGet("paged")]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> GetPaged(
            [FromQuery] string? keyword,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            pageNumber = Math.Max(pageNumber, 1);
            pageSize = pageSize is >= 1 and <= 50 ? pageSize : 5;

            var query = _context.Orders
                .Include(o => o.Customer)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim().ToLower();
                query = query.Where(o => o.OrderCode.ToLower().Contains(search) ||
                                         o.CashierUsername.ToLower().Contains(search) ||
                                         (o.Customer != null && o.Customer.CustomerName.ToLower().Contains(search)));
            }

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            int maxSafePage = (int.MaxValue / pageSize) + 1;
            pageNumber = Math.Min(pageNumber, maxSafePage);

            var items = await query
                .OrderByDescending(o => o.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new
                {
                    o.OrderId,
                    o.OrderCode,
                    o.CreatedDate,
                    o.CashierUsername,
                    o.CustomerId,
                    CustomerName = o.Customer != null ? o.Customer.CustomerName : "Khách vãng lai",
                    o.TotalItems,
                    o.FinalAmount,
                    o.RewardPoints,
                    o.RedeemedPoints,
                    o.DiscountAmount
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

        // Compatibility route for existing clients; shares the same ADO.NET transaction.
        [HttpPost("checkout")]
        [Authorize(Roles = "Admin,Cashier")]
        public IActionResult Checkout([FromBody] CheckoutRequestDto request)
        {
            try
            {
                var r = _checkout.Checkout(User?.Identity?.Name ?? request.CashierUsername,
                    request.CustomerId, request.CustomerPhone,
                    request.Items?.Select(i => new CartItemCheckoutDto { ProductId = i.ProductId, Quantity = i.Quantity }).ToList()!,
                    request.RedeemPoints);
                return Ok(new { success = true, orderId = r.OrderId, orderCode = r.OrderCode,
                    subtotal = r.Subtotal, discountAmount = r.DiscountAmount,
                    totalAmount = r.FinalAmount, finalAmount = r.FinalAmount, totalItems = r.TotalItems,
                    redeemedPoints = r.RedeemedPoints,
                    cashier = r.Cashier, customer = r.Customer, timestamp = r.Timestamp });
            }
            catch (Exception ex) when (ex is InvalidOperationException or OverflowException or System.Data.Common.DbException)
            { return BadRequest(new { success = false, message = "Thanh toán thất bại; không lưu thay đổi.", errorDetail = ex.Message }); }
        }
    }

    public class CheckoutRequestDto
    {
        public string CashierUsername { get; set; } = string.Empty;
        public int? CustomerId { get; set; }
        public string? CustomerPhone { get; set; }
        public int RedeemPoints { get; set; }
        public List<CheckoutItemDto> Items { get; set; } = new();
    }

    public class CheckoutItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
