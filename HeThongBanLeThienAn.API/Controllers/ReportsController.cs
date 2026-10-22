/*
* Dự án: Hệ Thống Bán Lẻ Thiên An - Buổi 5
* Mô tả: Controller ADO.NET thực thi Stored Procedure xuất báo cáo ca bán lẻ
* Bao gồm:
*   - Tiết 3: GET api/reports/revenue-by-cashier (Gọi Stored Procedure sp_GetRevenueByCashier)
*   - Bài tập 3.1: GET api/reports/revenue-by-date (Gọi Stored Procedure sp_GetRevenueByDateRange)
*   - Bài tập 3.2: GET api/reports/top-selling-products (Gọi Stored Procedure sp_GetTop5SellingProducts)
*   - Kèm truy vấn SQL tổng hợp để kiểm thử độc lập
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System.Data;
using System.Data.Common;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ReportsController : ControllerBase
    {
        private readonly IConfiguration? _configuration;
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly SupermarketDbContext? _context;

        [Microsoft.Extensions.DependencyInjection.ActivatorUtilitiesConstructor]
        public ReportsController(IConfiguration configuration, IDbConnectionFactory connectionFactory, SupermarketDbContext? context = null)
        {
            _configuration = configuration;
            _connectionFactory = connectionFactory;
            _context = context;
        }

        public ReportsController(SupermarketDbContext context)
        {
            _context = context;
            _connectionFactory = new DbConnectionFactory(new ConfigurationBuilder().AddInMemoryCollection(
                new Dictionary<string, string?> { ["ConnectionStrings:DefaultConnection"] = context.Database.GetConnectionString() }).Build());
        }

        // =========================================================================
        // TIẾT 3: BÁO CÁO DOANH THU THEO THU NGÂN QUA STORED PROCEDURE
        // GET: api/reports/revenue-by-cashier
        // =========================================================================
        [HttpGet("revenue-by-cashier")]
        public IActionResult GetRevenueByCashier()
        {
            var reportList = new List<CashierRevenueDto>();

            using (DbConnection connection = _connectionFactory.CreateConnection())
            {
                using (DbCommand command = connection.CreateCommand())
                {
                    // Nếu là SQL Server thì thực thi trực tiếp Stored Procedure
                    if (_connectionFactory.IsSqlServer)
                    {
                        command.CommandText = "sp_GetRevenueByCashier";
                        command.CommandType = CommandType.StoredProcedure;
                    }
                    else
                    {
                        // Fallback chỉ dùng bởi connection factory của project kiểm thử.
                        command.CommandText = @"
                            SELECT 
                                CashierUsername,
                                COUNT(OrderId) AS TotalOrders,
                                SUM(FinalAmount) AS TotalRevenue,
                                SUM(RewardPoints) AS TotalPointsAwarded
                            FROM Orders
                            GROUP BY CashierUsername
                            ORDER BY TotalRevenue DESC";
                        command.CommandType = CommandType.Text;
                    }

                    try
                    {
                        connection.Open();
                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new CashierRevenueDto
                                {
                                    CashierUsername = reader["CashierUsername"].ToString() ?? string.Empty,
                                    TotalOrders = Convert.ToInt32(reader["TotalOrders"]),
                                    TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]),
                                    TotalPointsAwarded = reader["TotalPointsAwarded"] != DBNull.Value 
                                        ? Convert.ToInt32(reader["TotalPointsAwarded"]) 
                                        : 0
                                };

                                reportList.Add(item);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, new 
                        { 
                            success = false,
                            message = "Lỗi thực thi Stored Procedure: " + ex.Message 
                        });
                    }
                }
            }

            return Ok(new
            {
                success = true,
                totalCashiers = reportList.Count,
                data = reportList
            });
        }

        // =========================================================================
        // BÀI TẬP 3.1: STORED PROCEDURE BÁO CÁO DOANH THU THEO KHOẢNG NGÀY
        // GET: api/reports/revenue-by-date?fromDate=2026-10-01&toDate=2026-10-31
        // =========================================================================
        [HttpGet("revenue-by-date")]
        public IActionResult GetRevenueByDateRange([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            var start = fromDate ?? DateTime.Today.AddDays(-30);
            var end = (toDate ?? DateTime.Today).Date.AddDays(1);
            if (start >= end) return BadRequest(new { message = "Khoảng ngày không hợp lệ." });

            var reportList = new List<CashierRevenueDto>();

            using (DbConnection connection = _connectionFactory.CreateConnection())
            {
                using (DbCommand command = connection.CreateCommand())
                {
                    if (_connectionFactory.IsSqlServer)
                    {
                        command.CommandText = "sp_GetRevenueByDateRange";
                        command.CommandType = CommandType.StoredProcedure;
                    }
                    else
                    {
                        command.CommandText = @"
                            SELECT 
                                CashierUsername,
                                COUNT(OrderId) AS TotalOrders,
                                SUM(FinalAmount) AS TotalRevenue,
                                SUM(RewardPoints) AS TotalPointsAwarded
                            FROM Orders
                            WHERE CreatedDate >= @FromDate AND CreatedDate < @ToDate
                            GROUP BY CashierUsername
                            ORDER BY TotalRevenue DESC";
                        command.CommandType = CommandType.Text;
                    }

                    command.Parameters.Add(_connectionFactory.CreateParameter("@FromDate", start));
                    command.Parameters.Add(_connectionFactory.CreateParameter("@ToDate", end));

                    try
                    {
                        connection.Open();
                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reportList.Add(new CashierRevenueDto
                                {
                                    CashierUsername = reader["CashierUsername"].ToString() ?? string.Empty,
                                    TotalOrders = Convert.ToInt32(reader["TotalOrders"]),
                                    TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]),
                                    TotalPointsAwarded = reader["TotalPointsAwarded"] != DBNull.Value 
                                        ? Convert.ToInt32(reader["TotalPointsAwarded"]) 
                                        : 0
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, new 
                        { 
                            success = false,
                            message = "Lỗi thực thi Stored Procedure: " + ex.Message 
                        });
                    }
                }
            }

            return Ok(new
            {
                success = true,
                fromDate = start.ToString("yyyy-MM-dd"),
                toDate = end.ToString("yyyy-MM-dd"),
                data = reportList
            });
        }

        // =========================================================================
        // BÀI TẬP 3.2: STORED PROCEDURE TOP 5 SẢN PHẨM BÁN CHẠY NHẤT
        // GET: api/reports/top-selling-products
        // =========================================================================
        [HttpGet("top-selling-products")]
        public IActionResult GetTop5SellingProducts()
        {
            var topList = new List<TopSellingProductDto>();

            using (DbConnection connection = _connectionFactory.CreateConnection())
            {
                using (DbCommand command = connection.CreateCommand())
                {
                    if (_connectionFactory.IsSqlServer)
                    {
                        command.CommandText = "sp_GetTop5SellingProducts";
                        command.CommandType = CommandType.StoredProcedure;
                    }
                    else
                    {
                        command.CommandText = @"
                            SELECT 
                                p.ProductId,
                                p.Barcode,
                                p.ProductName,
                                p.Price,
                                SUM(od.Quantity) AS TotalSoldQuantity,
                                SUM(od.LineTotal) AS TotalSalesAmount
                            FROM OrderDetails od
                            INNER JOIN Products p ON od.ProductId = p.ProductId
                            GROUP BY p.ProductId, p.Barcode, p.ProductName, p.Price
                            ORDER BY TotalSoldQuantity DESC, TotalSalesAmount DESC
                            LIMIT 5";
                        command.CommandType = CommandType.Text;
                    }

                    try
                    {
                        connection.Open();
                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                topList.Add(new TopSellingProductDto
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString() ?? string.Empty,
                                    ProductName = reader["ProductName"].ToString() ?? string.Empty,
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    TotalSoldQuantity = Convert.ToInt32(reader["TotalSoldQuantity"]),
                                    TotalSalesAmount = Convert.ToDecimal(reader["TotalSalesAmount"])
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, new 
                        { 
                            success = false,
                            message = "Lỗi thực thi Stored Procedure Top 5: " + ex.Message 
                        });
                    }
                }
            }

            return Ok(new
            {
                success = true,
                totalItems = topList.Count,
                data = topList
            });
        }

        // Tương thích ngược với endpoint cũ GET api/reports/quick
        [HttpGet("quick")]
        public async Task<IActionResult> GetQuickReport([FromQuery] string? date)
        {
            DateTime queryDate = DateTime.Today;
            if (!string.IsNullOrEmpty(date) && DateTime.TryParse(date, out DateTime parsedDate))
            {
                queryDate = parsedDate.Date;
            }

            if (_context == null) return StatusCode(500, new { message = "Thiếu kết nối báo cáo." });
            var nextDay = queryDate.AddDays(1);
            var orders = await _context.Orders.AsNoTracking()
                .Where(o => o.CreatedDate >= queryDate && o.CreatedDate < nextDay)
                .Select(o => new { o.OrderId, o.FinalAmount }).ToListAsync();
            var orderIds = orders.Select(o => o.OrderId).ToList();
            var bestSeller = await _context.OrderDetails.AsNoTracking()
                .Where(d => orderIds.Contains(d.OrderId))
                .GroupBy(d => new { d.ProductId, d.Product!.ProductName })
                .OrderByDescending(g => g.Sum(d => d.Quantity)).ThenBy(g => g.Key.ProductId)
                .Select(g => g.Key.ProductName).FirstOrDefaultAsync();
            return Ok(new
            {
                ReportDate = queryDate.ToString("yyyy-MM-dd"),
                TotalOrders = orders.Count,
                TotalRevenue = orders.Sum(o => o.FinalAmount),
                BestSeller = bestSeller ?? "Chưa có giao dịch"
            });
        }
    }
}
