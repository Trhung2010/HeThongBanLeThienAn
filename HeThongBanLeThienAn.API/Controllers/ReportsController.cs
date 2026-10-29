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

            return Ok(new CashierRevenueReportDto
            {
                Success = true,
                TotalCashiers = reportList.Count,
                Data = reportList
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

            return Ok(new DateRangeRevenueReportDto
            {
                Success = true,
                FromDate = start.ToString("yyyy-MM-dd"),
                ToDate = end.ToString("yyyy-MM-dd"),
                Data = reportList
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

            return Ok(new TopSellingReportDto
            {
                Success = true,
                TotalItems = topList.Count,
                Data = topList
            });
        }

        [HttpGet("dashboard-summary")]
        public async Task<IActionResult> GetDashboardSummary(
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate)
        {
            if (_context == null) return StatusCode(500, new { message = "Thiếu kết nối báo cáo." });

            DateTime start = (fromDate ?? DateTime.Today.AddDays(-6)).Date;
            DateTime endInclusive = (toDate ?? DateTime.Today).Date;
            if (start > endInclusive)
                return BadRequest(new { message = "Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc." });
            if ((endInclusive - start).TotalDays > 366)
                return BadRequest(new { message = "Khoảng báo cáo tối đa là 366 ngày." });

            DateTime endExclusive = endInclusive.AddDays(1);
            var orders = await _context.Orders.AsNoTracking()
                .Where(o => o.CreatedDate >= start && o.CreatedDate < endExclusive)
                .Select(o => new { o.OrderId, o.CreatedDate, o.FinalAmount })
                .ToListAsync();
            var orderIds = orders.Select(o => o.OrderId).ToList();

            var detailRows = await _context.OrderDetails.AsNoTracking()
                .Where(d => orderIds.Contains(d.OrderId))
                .Select(d => new
                {
                    d.ProductId,
                    d.Product!.Barcode,
                    d.Product.ProductName,
                    d.Product.Price,
                    d.Quantity,
                    d.LineTotal
                })
                .ToListAsync();

            // Group on the client so the same query works on SQL Server and the
            // SQLite provider used by regression tests (SQLite cannot SUM decimal).
            var topSelling = detailRows
                .GroupBy(d => new { d.ProductId, d.Barcode, d.ProductName, d.Price })
                .Select(g => new TopSellingProductDto
                {
                    ProductId = g.Key.ProductId,
                    Barcode = g.Key.Barcode,
                    ProductName = g.Key.ProductName,
                    Price = g.Key.Price,
                    TotalSoldQuantity = g.Sum(d => d.Quantity),
                    TotalSalesAmount = g.Sum(d => d.LineTotal)
                })
                .OrderByDescending(x => x.TotalSoldQuantity)
                .ThenByDescending(x => x.TotalSalesAmount)
                .Take(5)
                .ToList();

            var revenueByDate = orders
                .GroupBy(o => o.CreatedDate.Date)
                .ToDictionary(g => g.Key, g => new
                {
                    Revenue = g.Sum(o => o.FinalAmount),
                    Orders = g.Count()
                });

            var series = new List<RevenuePointDto>();
            for (DateTime day = start; day <= endInclusive; day = day.AddDays(1))
            {
                revenueByDate.TryGetValue(day, out var summary);
                series.Add(new RevenuePointDto
                {
                    Date = day,
                    Revenue = summary?.Revenue ?? 0,
                    Orders = summary?.Orders ?? 0
                });
            }

            return Ok(new DashboardSummaryDto
            {
                FromDate = start,
                ToDate = endInclusive,
                RevenueToday = orders.Sum(o => o.FinalAmount),
                TotalOrders = orders.Count,
                NewCustomers = await _context.Customers.AsNoTracking()
                    .CountAsync(c => c.CreatedDate >= start && c.CreatedDate < endExclusive),
                LowStockItems = await _context.Products.AsNoTracking()
                    .CountAsync(p => p.StockQuantity <= 10),
                RevenueSeries = series,
                TopSellingProducts = topSelling
            });
        }

        // Endpoint cũ vẫn hoạt động cho client đã phát hành.
        [HttpGet("quick")]
        public async Task<IActionResult> GetQuickReport([FromQuery] string? date)
        {
            DateTime queryDate = DateTime.Today;
            if (!string.IsNullOrWhiteSpace(date) && !DateTime.TryParse(date, out queryDate))
                return BadRequest(new { message = "Ngày báo cáo không hợp lệ." });

            IActionResult dashboardResult = await GetDashboardSummary(queryDate.Date, queryDate.Date);
            if (dashboardResult is not OkObjectResult { Value: DashboardSummaryDto dashboard })
                return dashboardResult;

            return Ok(new QuickReportDto
            {
                TotalOrders = dashboard.TotalOrders,
                TotalRevenue = dashboard.RevenueToday,
                BestSeller = dashboard.TopSellingProducts.FirstOrDefault()?.ProductName ?? "Chưa có dữ liệu",
                NewCustomers = dashboard.NewCustomers,
                LowStockItems = dashboard.LowStockItems
            });
        }
    }
}
