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
                    request.Items?.Select(i => new CartItemCheckoutDto { ProductId = i.ProductId, Quantity = i.Quantity }).ToList()!);
                return Ok(new { success = true, orderId = r.OrderId, orderCode = r.OrderCode,
                    totalAmount = r.FinalAmount, finalAmount = r.FinalAmount, totalItems = r.TotalItems,
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
        public List<CheckoutItemDto> Items { get; set; } = new();
    }

    public class CheckoutItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
