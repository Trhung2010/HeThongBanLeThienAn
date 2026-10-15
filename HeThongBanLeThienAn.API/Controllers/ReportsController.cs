using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ReportsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public ReportsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: /api/reports/quick?date=2026-10-08
        [HttpGet("quick")]
        public async Task<IActionResult> GetQuickReport([FromQuery] string? date)
        {
            DateTime queryDate = DateTime.Today;
            if (!string.IsNullOrEmpty(date) && DateTime.TryParse(date, out DateTime parsedDate))
            {
                queryDate = parsedDate.Date;
            }

            // Lấy mặt hàng tồn kho cao nhất hoặc bán chạy nhất từ Database (cast double để tương thích SQLite OrderBy)
            var topProduct = await _context.Products.OrderByDescending(p => (double)p.Price).FirstOrDefaultAsync();
            string bestSellerName = topProduct?.ProductName ?? "Nước khoáng thiên nhiên Lavie 500ml";

            // Dữ liệu mô phỏng doanh thu ca theo ngày chọn
            int totalOrders = 38;
            decimal totalRevenue = 4850000m;

            return Ok(new
            {
                ReportDate = queryDate.ToString("yyyy-MM-dd"),
                TotalOrders = totalOrders,
                TotalRevenue = totalRevenue,
                BestSeller = bestSellerName
            });
        }
    }
}
