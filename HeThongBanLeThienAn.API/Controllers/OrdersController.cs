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
                .Select(o => new OrderSummaryDto
                {
                    OrderId = o.OrderId,
                    OrderCode = o.OrderCode,
                    CreatedDate = o.CreatedDate,
                    CashierUsername = o.CashierUsername,
                    CustomerId = o.CustomerId,
                    CustomerName = o.Customer != null ? o.Customer.CustomerName : "Khách vãng lai",
                    TotalItems = o.TotalItems,
                    FinalAmount = o.FinalAmount,
                    RewardPoints = o.RewardPoints,
                    RedeemedPoints = o.RedeemedPoints,
                    DiscountAmount = o.DiscountAmount
                })
                .ToListAsync();

            return Ok(new PagedResult<OrderSummaryDto>
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
                .AsNoTracking()
                .OrderByDescending(o => o.CreatedDate)
                .Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    OrderCode = o.OrderCode,
                    CreatedDate = o.CreatedDate,
                    CustomerId = o.CustomerId,
                    CustomerName = o.Customer != null ? o.Customer.CustomerName : "Khách vãng lai",
                    CashierUsername = o.CashierUsername,
                    TotalItems = o.TotalItems,
                    FinalAmount = o.FinalAmount,
                    RewardPoints = o.RewardPoints,
                    RedeemedPoints = o.RedeemedPoints,
                    DiscountAmount = o.DiscountAmount,
                    OrderDetails = o.OrderDetails
                        .OrderBy(d => d.OrderDetailId)
                        .Select(d => new OrderDetailDto
                        {
                            OrderDetailId = d.OrderDetailId,
                            ProductId = d.ProductId,
                            Barcode = d.Product == null ? string.Empty : d.Product.Barcode,
                            ProductName = d.Product == null ? string.Empty : d.Product.ProductName,
                            Quantity = d.Quantity,
                            UnitPrice = d.UnitPrice,
                            LineTotal = d.LineTotal
                        })
                        .ToList()
                })
                .ToListAsync();

            return Ok(orders);
        }

        // GET /api/orders/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _context.Orders
                .AsNoTracking()
                .Where(o => o.OrderId == id)
                .Select(o => new OrderDto
                {
                    OrderId = o.OrderId,
                    OrderCode = o.OrderCode,
                    CreatedDate = o.CreatedDate,
                    CustomerId = o.CustomerId,
                    CustomerName = o.Customer != null ? o.Customer.CustomerName : "Khách vãng lai",
                    CashierUsername = o.CashierUsername,
                    TotalItems = o.TotalItems,
                    FinalAmount = o.FinalAmount,
                    RewardPoints = o.RewardPoints,
                    RedeemedPoints = o.RedeemedPoints,
                    DiscountAmount = o.DiscountAmount,
                    OrderDetails = o.OrderDetails
                        .OrderBy(d => d.OrderDetailId)
                        .Select(d => new OrderDetailDto
                        {
                            OrderDetailId = d.OrderDetailId,
                            ProductId = d.ProductId,
                            Barcode = d.Product == null ? string.Empty : d.Product.Barcode,
                            ProductName = d.Product == null ? string.Empty : d.Product.ProductName,
                            Quantity = d.Quantity,
                            UnitPrice = d.UnitPrice,
                            LineTotal = d.LineTotal
                        })
                        .ToList()
                })
                .SingleOrDefaultAsync();

            if (order == null)
            {
                return NotFound(new { message = "Không tìm thấy đơn hàng!" });
            }

            return Ok(order);
        }

        // Compatibility route for existing clients; shares the same ADO.NET transaction.
        [HttpPost("checkout")]
        [Authorize(Roles = "Admin,Cashier")]
        public IActionResult Checkout([FromBody] PosCheckoutRequestDto request)
        {
            try
            {
                var r = _checkout.Checkout(User?.Identity?.Name ?? request.CashierUsername,
                    request.CustomerId, request.CustomerPhone,
                    request.Items,
                    request.RedeemPoints);
                return Ok(new CheckoutResponseDto
                {
                    Success = true,
                    OrderId = r.OrderId,
                    OrderCode = r.OrderCode,
                    Subtotal = r.Subtotal,
                    DiscountAmount = r.DiscountAmount,
                    TotalAmount = r.FinalAmount,
                    FinalAmount = r.FinalAmount,
                    TotalItems = r.TotalItems,
                    RedeemedPoints = r.RedeemedPoints,
                    Cashier = r.Cashier,
                    Customer = r.Customer,
                    Timestamp = r.Timestamp
                });
            }
            catch (Exception ex) when (ex is InvalidOperationException or OverflowException or System.Data.Common.DbException)
            { return BadRequest(new { success = false, message = "Thanh toán thất bại; không lưu thay đổi.", errorDetail = ex.Message }); }
        }
    }

}
