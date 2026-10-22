/*
* Dự án: Hệ Thống Bán Lẻ Thiên An - Buổi 5
* Mô tả: Controller ADO.NET thuần - Tra cứu mã vạch siêu tốc và demo DataTable
* Bao gồm:
*   - Tiết 2 Thao tác 1: Tra cứu theo mã vạch (Connected Mode - DbDataReader / SqlDataReader)
*   - Tiết 2 Thao tác 2: Tải danh sách lưu đệm (Disconnected Mode - DataTable & SqlDataAdapter)
*   - Bài tập 2.1: Endpoint lọc sản phẩm theo mức tồn kho (minStock)
*   - Bài tập 2.2: Đếm số lượng sản phẩm theo từng nhóm danh mục bằng DataTable
*/

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System.Data;
using System.Data.Common;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/ado-products")]
    [ApiController]
    [Authorize(Roles = "Admin,Cashier,Warehouse")]
    public class AdoProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnectionFactory _connectionFactory;

        public AdoProductController(IConfiguration configuration, IDbConnectionFactory connectionFactory)
        {
            _configuration = configuration;
            _connectionFactory = connectionFactory;
        }

        // =========================================================================
        // THAO TÁC 1: TRA CỨU THEO MÃ VẠCH (CONNECTED MODE - SQLDATAREADER)
        // =========================================================================
        // GET: api/ado-products/barcode/893456789001
        [HttpGet("barcode/{barcode}")]
        public IActionResult GetProductByBarcode(string barcode)
        {
            // 1. Kiểm tra tính hợp lệ của tham số đầu vào
            if (string.IsNullOrWhiteSpace(barcode))
            {
                return BadRequest(new { message = "Mã vạch không được để trống!" });
            }

            // 2. Sử dụng khối using để đảm bảo DbConnection luôn được đóng và giải phóng
            using (DbConnection connection = _connectionFactory.CreateConnection())
            {
                // 3. Câu lệnh SELECT có tham số hóa @Barcode chống SQL Injection
                string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity 
                                FROM Products 
                                WHERE Barcode = @Barcode";

                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;

                    // 4. Gán giá trị an toàn qua Parameter
                    var param = _connectionFactory.CreateParameter("@Barcode", barcode.Trim());
                    command.Parameters.Add(param);

                    try
                    {
                        // 5. Mở kết nối vật lý tới CSDL
                        connection.Open();

                        // 6. Khởi tạo luồng đọc nhanh Forward-Only DataReader
                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            // 7. Đọc bản ghi: reader.Read() trả về true nếu tìm thấy dòng dữ liệu
                            if (reader.Read())
                            {
                                var productDto = new ProductLookupDto
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString() ?? string.Empty,
                                    ProductName = reader["ProductName"].ToString() ?? string.Empty,
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"])
                                };

                                // Trả về mã 200 OK kèm dữ liệu sản phẩm
                                return Ok(productDto);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Bắt lỗi nếu kết nối CSDL gặp sự cố
                        return StatusCode(500, new { message = "Lỗi truy vấn SQL: " + ex.Message });
                    }
                }
            } // Kết thúc using, connection.Close() tự động được thực thi tại đây

            // 8. Nếu không tìm thấy sản phẩm nào khớp mã vạch -> Trả về 404 Not Found
            return NotFound(new { message = $"Không tìm thấy sản phẩm có mã vạch: {barcode}" });
        }

        // =========================================================================
        // THAO TÁC 2: TẢI DANH SÁCH LƯU ĐỆM (DISCONNECTED MODE - DATATABLE & ADAPTER)
        // =========================================================================
        // GET: api/ado-products/disconnected-cache
        [HttpGet("disconnected-cache")]
        public IActionResult GetProductsDisconnected()
        {
            // 1. Khởi tạo bảng ảo DataTable trên RAM
            DataTable productTable = new DataTable("CachedProducts");

            // 2. Mở kết nối và nạp dữ liệu vào DataTable
            using (DbConnection connection = _connectionFactory.CreateConnection())
            {
                string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity 
                                FROM Products 
                                ORDER BY ProductId ASC";

                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;

                    if (_connectionFactory.IsSqlServer && command is SqlCommand sqlCmd)
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd))
                        {
                            adapter.Fill(productTable);
                        }
                    }
                    else
                    {
                        connection.Open();
                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            productTable.Load(reader);
                        }
                    }
                }
            } // Tại đây, kết nối CSDL ĐÃ ĐƯỢC ĐÓNG HOÀN TOÀN!

            // 3. Thao tác độc lập trên bộ nhớ RAM (Không cần kết nối CSDL nữa)
            var resultList = new List<ProductLookupDto>();

            foreach (DataRow row in productTable.Rows)
            {
                resultList.Add(new ProductLookupDto
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    Barcode = row["Barcode"].ToString() ?? string.Empty,
                    ProductName = row["ProductName"].ToString() ?? string.Empty,
                    Price = Convert.ToDecimal(row["Price"]),
                    StockQuantity = Convert.ToInt32(row["StockQuantity"])
                });
            }

            // Trả về danh sách sản phẩm đã được lưu đệm trên RAM
            return Ok(new
            {
                totalLoaded = productTable.Rows.Count,
                cachedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                data = resultList
            });
        }

        // =========================================================================
        // BÀI TẬP 2.1: TRA CỨU THEO KHOẢNG TỒN KHO CẢNH BÁO (SQLDATAREADER)
        // =========================================================================
        // GET: api/ado-products/filter-stock?minStock=50
        [HttpGet("filter-stock")]
        public IActionResult FilterStock([FromQuery] int minStock = 50)
        {
            var resultList = new List<ProductLookupDto>();

            using (DbConnection connection = _connectionFactory.CreateConnection())
            {
                string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity 
                                FROM Products 
                                WHERE StockQuantity <= @MinStock 
                                ORDER BY StockQuantity ASC";

                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Parameters.Add(_connectionFactory.CreateParameter("@MinStock", minStock));

                    try
                    {
                        connection.Open();
                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                resultList.Add(new ProductLookupDto
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString() ?? string.Empty,
                                    ProductName = reader["ProductName"].ToString() ?? string.Empty,
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"])
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, new { message = "Lỗi truy vấn SQL: " + ex.Message });
                    }
                }
            }

            return Ok(new
            {
                minStockThreshold = minStock,
                totalFound = resultList.Count,
                data = resultList
            });
        }

        // =========================================================================
        // BÀI TẬP 2.2: ĐẾM SỐ LƯỢNG SẢN PHẨM THEO TỪNG NHÓM DANH MỤC (DATATABLE)
        // =========================================================================
        // GET: api/ado-products/count-by-category
        [HttpGet("count-by-category")]
        public IActionResult CountByCategory()
        {
            DataTable categoryTable = new DataTable("CategoryProductCount");

            using (DbConnection connection = _connectionFactory.CreateConnection())
            {
                string query = @"SELECT CategoryId, COUNT(*) AS Total 
                                FROM Products 
                                GROUP BY CategoryId 
                                ORDER BY CategoryId ASC";

                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;

                    if (_connectionFactory.IsSqlServer && command is SqlCommand sqlCmd)
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd))
                        {
                            adapter.Fill(categoryTable);
                        }
                    }
                    else
                    {
                        connection.Open();
                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            categoryTable.Load(reader);
                        }
                    }
                }
            }

            var result = new List<CategoryProductCountDto>();
            foreach (DataRow row in categoryTable.Rows)
            {
                result.Add(new CategoryProductCountDto
                {
                    CategoryId = Convert.ToInt32(row["CategoryId"]),
                    Total = Convert.ToInt32(row["Total"])
                });
            }

            return Ok(new
            {
                totalCategories = categoryTable.Rows.Count,
                data = result
            });
        }
    }
}
