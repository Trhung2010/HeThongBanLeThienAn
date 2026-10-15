using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers;

[Route("api/checkout")]
[ApiController]
[Authorize(Roles = "Admin,Cashier")]
public class CheckoutController : ControllerBase
{
    private readonly CheckoutService _service;
    public CheckoutController(IConfiguration configuration, IDbConnectionFactory factory) => _service = new(factory);

    [HttpPost]
    public IActionResult ProcessCheckout([FromBody] PosCheckoutRequestDto request)
    {
        try
        {
            string cashier = User?.Identity?.Name ?? request.CashierUsername;
            var r = _service.Checkout(cashier, request.CustomerId, request.CustomerPhone, request.Items);
            return Ok(new { success = true, orderId = r.OrderId, orderCode = r.OrderCode,
                totalItems = r.TotalItems, finalAmount = r.FinalAmount, totalAmount = r.FinalAmount,
                rewardPointsAdded = r.RewardPointsAdded, customer = r.Customer, timestamp = r.Timestamp });
        }
        catch (Exception ex) when (ex is InvalidOperationException or OverflowException or System.Data.Common.DbException)
        { return BadRequest(new { success = false, message = "Thanh toán thất bại; không lưu thay đổi.", errorDetail = ex.Message }); }
    }

    [HttpDelete("cancel/{orderId}")]
    [Authorize(Roles = "Admin")]
    public IActionResult CancelOrder(int orderId)
    {
        try { _service.Cancel(orderId); return Ok(new { success = true, message = "Đã hủy hóa đơn và hoàn trả kho, điểm." }); }
        catch (Exception ex) when (ex is InvalidOperationException or System.Data.Common.DbException)
        { return BadRequest(new { success = false, message = "Hủy hóa đơn thất bại; không lưu thay đổi.", errorDetail = ex.Message }); }
    }
}
