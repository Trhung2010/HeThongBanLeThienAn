/*
* Dự án: Hệ Thống Bán Lẻ Thiên An - Buổi 5
* Mô tả: API Thanh toán POS đa bảng được bảo vệ bởi Database Transaction (chuẩn ACID)
* Bao gồm:
*   - Tiết 5: POST api/checkout (Thanh toán hóa đơn, trừ tồn kho và tích điểm trong 1 Transaction)
*   - Bài tập mở rộng (Tiết 5): DELETE api/checkout/cancel/{orderId} (Hủy hóa đơn, hoàn trả tồn kho và trừ điểm tích lũy trong 1 Transaction)
*/

using Microsoft.AspNetCore.Mvc;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System.Data.Common;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnectionFactory _connectionFactory;

        public CheckoutController(IConfiguration configuration, IDbConnectionFactory connectionFactory)
        {
            _configuration = configuration;
            _connectionFactory = connectionFactory;
        }

        // =========================================================================
        // TIẾT 5: POST api/checkout
        // Chức năng: Thanh toán hóa đơn, trừ tồn kho và tích điểm trong 1 Transaction
        // =========================================================================
        [HttpPost]
        public IActionResult ProcessCheckout([FromBody] PosCheckoutRequestDto request)
        {
            // 1. Kiểm tra tính hợp lệ của giỏ hàng
            if (request.Items == null || request.Items.Count == 0)
            {
                return BadRequest(new { message = "Giỏ hàng rỗng, không thể tiến hành thanh toán!" });
            }

            if (string.IsNullOrWhiteSpace(request.CashierUsername))
            {
                return BadRequest(new { message = "Thiếu thông tin tài khoản thu ngân lập hóa đơn!" });
            }

            using (DbConnection connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                // 2. KHỞI TẠO TRANSACTION (Bắt đầu phiên giao dịch khép kín bảo vệ ACID)
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Tính toán tổng tiền, tổng số lượng và điểm thưởng tích lũy
                    decimal finalAmount = request.Items.Sum(i => i.Quantity * i.UnitPrice);
                    int totalItems = request.Items.Sum(i => i.Quantity);

                    // Quy tắc tích điểm siêu thị: Cứ mỗi 10.000đ mua hàng được tặng 1 điểm thưởng
                    int rewardPoints = (int)(finalAmount / 10000);

                    // Sinh mã hóa đơn theo cấu trúc: HD-YYYYMMDD-XXXX (4 ký tự ngẫu nhiên)
                    string orderCode = $"HD-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";

                    // =====================================================================
                    // BƯỚC 1: TẠO HÓA ĐƠN TỔNG THỂ TRONG BẢNG Orders
                    // =====================================================================
                    int orderId = 0;
                    if (_connectionFactory.IsSqlServer)
                    {
                        string insertOrderSql = @"
                            INSERT INTO Orders (OrderCode, CreatedDate, CashierUsername, CustomerId, TotalItems, FinalAmount, RewardPoints)
                            OUTPUT INSERTED.OrderId
                            VALUES (@OrderCode, GETDATE(), @CashierUsername, @CustomerId, @TotalItems, @FinalAmount, @RewardPoints)";

                        using (DbCommand cmdOrder = connection.CreateCommand())
                        {
                            cmdOrder.Transaction = transaction;
                            cmdOrder.CommandText = insertOrderSql;
                            cmdOrder.Parameters.Add(_connectionFactory.CreateParameter("@OrderCode", orderCode));
                            cmdOrder.Parameters.Add(_connectionFactory.CreateParameter("@CashierUsername", request.CashierUsername.Trim()));
                            cmdOrder.Parameters.Add(_connectionFactory.CreateParameter("@CustomerId", request.CustomerId));
                            cmdOrder.Parameters.Add(_connectionFactory.CreateParameter("@TotalItems", totalItems));
                            cmdOrder.Parameters.Add(_connectionFactory.CreateParameter("@FinalAmount", finalAmount));
                            cmdOrder.Parameters.Add(_connectionFactory.CreateParameter("@RewardPoints", rewardPoints));

                            orderId = Convert.ToInt32(cmdOrder.ExecuteScalar());
                        }
                    }
                    else
                    {
                        // Cho SQLite: Sử dụng datetime('now') và last_insert_rowid()
                        string insertOrderSql = @"
                            INSERT INTO Orders (OrderCode, CreatedDate, CashierUsername, CustomerId, TotalItems, FinalAmount, RewardPoints)
                            VALUES (@OrderCode, datetime('now'), @CashierUsername, @CustomerId, @TotalItems, @FinalAmount, @RewardPoints);
                            SELECT last_insert_rowid();";

                        using (DbCommand cmdOrder = connection.CreateCommand())
                        {
                            cmdOrder.Transaction = transaction;
                            cmdOrder.CommandText = insertOrderSql;
                            cmdOrder.Parameters.Add(_connectionFactory.CreateParameter("@OrderCode", orderCode));
                            cmdOrder.Parameters.Add(_connectionFactory.CreateParameter("@CashierUsername", request.CashierUsername.Trim()));
                            cmdOrder.Parameters.Add(_connectionFactory.CreateParameter("@CustomerId", request.CustomerId));
                            cmdOrder.Parameters.Add(_connectionFactory.CreateParameter("@TotalItems", totalItems));
                            cmdOrder.Parameters.Add(_connectionFactory.CreateParameter("@FinalAmount", finalAmount));
                            cmdOrder.Parameters.Add(_connectionFactory.CreateParameter("@RewardPoints", rewardPoints));

                            orderId = Convert.ToInt32(cmdOrder.ExecuteScalar());
                        }
                    }

                    // =====================================================================
                    // BƯỚC 2 & 3: XỬ LÝ TỪNG MÓN HÀNG (KIỂM TRA, LƯU CHI TIẾT VÀ TRỪ TỒN KHO)
                    // =====================================================================
                    foreach (var item in request.Items)
                    {
                        // 3.1. Kiểm tra số lượng tồn kho thực tế trước khi trừ
                        string checkStockSql = "SELECT StockQuantity, ProductName FROM Products WHERE ProductId = @ProductId";
                        int currentStock = 0;
                        string productName = string.Empty;

                        using (DbCommand cmdCheck = connection.CreateCommand())
                        {
                            cmdCheck.Transaction = transaction;
                            cmdCheck.CommandText = checkStockSql;
                            cmdCheck.Parameters.Add(_connectionFactory.CreateParameter("@ProductId", item.ProductId));

                            using (DbDataReader reader = cmdCheck.ExecuteReader())
                            {
                                if (!reader.Read())
                                {
                                    throw new InvalidOperationException($"Không tìm thấy sản phẩm có ID: {item.ProductId}");
                                }
                                currentStock = Convert.ToInt32(reader["StockQuantity"]);
                                productName = reader["ProductName"].ToString() ?? string.Empty;
                            }
                        }

                        // 3.2. ĐIỂM KIỂM SOÁT LOGIC: Nếu số lượng mua vượt quá tồn kho -> Kích hoạt Exception
                        if (currentStock < item.Quantity)
                        {
                            throw new InvalidOperationException(
                                $"Sản phẩm '{productName}' (ID: {item.ProductId}) không đủ tồn kho! " +
                                $"(Tồn kho thực tế: {currentStock}, Số lượng yêu cầu mua: {item.Quantity})");
                        }

                        // 3.3. Thêm dòng chi tiết vào bảng OrderDetails
                        string insertDetailSql = @"
                            INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, LineTotal)
                            VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice, @LineTotal)";

                        using (DbCommand cmdDetail = connection.CreateCommand())
                        {
                            cmdDetail.Transaction = transaction;
                            cmdDetail.CommandText = insertDetailSql;
                            cmdDetail.Parameters.Add(_connectionFactory.CreateParameter("@OrderId", orderId));
                            cmdDetail.Parameters.Add(_connectionFactory.CreateParameter("@ProductId", item.ProductId));
                            cmdDetail.Parameters.Add(_connectionFactory.CreateParameter("@Quantity", item.Quantity));
                            cmdDetail.Parameters.Add(_connectionFactory.CreateParameter("@UnitPrice", item.UnitPrice));
                            cmdDetail.Parameters.Add(_connectionFactory.CreateParameter("@LineTotal", item.Quantity * item.UnitPrice));
                            cmdDetail.ExecuteNonQuery();
                        }

                        // 3.4. Khấu trừ số lượng tồn kho trong bảng Products
                        string updateStockSql = @"
                            UPDATE Products 
                            SET StockQuantity = StockQuantity - @Qty 
                            WHERE ProductId = @ProductId";

                        using (DbCommand cmdStock = connection.CreateCommand())
                        {
                            cmdStock.Transaction = transaction;
                            cmdStock.CommandText = updateStockSql;
                            cmdStock.Parameters.Add(_connectionFactory.CreateParameter("@Qty", item.Quantity));
                            cmdStock.Parameters.Add(_connectionFactory.CreateParameter("@ProductId", item.ProductId));
                            cmdStock.ExecuteNonQuery();
                        }
                    }

                    // =====================================================================
                    // BƯỚC 4: CỘNG ĐIỂM TÍCH LŨY CHO KHÁCH HÀNG TRONG BẢNG Customers
                    // =====================================================================
                    if (rewardPoints > 0 && request.CustomerId > 0)
                    {
                        string updatePointsSql = @"
                            UPDATE Customers 
                            SET RewardPoints = RewardPoints + @Points 
                            WHERE CustomerId = @CustomerId";

                        using (DbCommand cmdCustomer = connection.CreateCommand())
                        {
                            cmdCustomer.Transaction = transaction;
                            cmdCustomer.CommandText = updatePointsSql;
                            cmdCustomer.Parameters.Add(_connectionFactory.CreateParameter("@Points", rewardPoints));
                            cmdCustomer.Parameters.Add(_connectionFactory.CreateParameter("@CustomerId", request.CustomerId));
                            cmdCustomer.ExecuteNonQuery();
                        }
                    }

                    // =====================================================================
                    // BƯỚC 5: XÁC NHẬN LƯU VĨNH VIỄN TOÀN BỘ THAO TÁC (COMMIT)
                    // =====================================================================
                    transaction.Commit();

                    return Ok(new
                    {
                        success = true,
                        message = "Thanh toán hóa đơn POS thành công!",
                        orderId = orderId,
                        orderCode = orderCode,
                        totalItems = totalItems,
                        finalAmount = finalAmount,
                        rewardPointsAdded = rewardPoints
                    });
                }
                catch (Exception ex)
                {
                    // =====================================================================
                    // BƯỚC 6: HOÀN TÁC TOÀN BỘ DỮ LIỆU SẠCH SẼ KHI CÓ BẤT KỲ LỖI NÀO (ROLLBACK)
                    // =====================================================================
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception rollbackEx)
                    {
                        return StatusCode(500, new 
                        { 
                            success = false,
                            message = "Lỗi nghiêm trọng khi Rollback giao dịch!",
                            rollbackError = rollbackEx.Message 
                        });
                    }

                    return BadRequest(new
                    {
                        success = false,
                        message = "Giao dịch thanh toán bị hủy bỏ do phát sinh lỗi!",
                        errorDetail = ex.Message
                    });
                }
            }
        }

        // =========================================================================
        // BÀI TẬP MỞ RỘNG (TIẾT 5): HỦY ĐƠN HÀNG VÀ HOÀN TRẢ TỒN KHO TỰ ĐỘNG
        // DELETE: api/checkout/cancel/{orderId}
        // =========================================================================
        [HttpDelete("cancel/{orderId}")]
        public IActionResult CancelOrder(int orderId)
        {
            using (DbConnection connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    // 1. Kiểm tra hóa đơn và lấy thông tin khách hàng, điểm thưởng
                    string getOrderSql = "SELECT CustomerId, RewardPoints FROM Orders WHERE OrderId = @OrderId";
                    int customerId = 0;
                    int pointsToDeduct = 0;

                    using (DbCommand cmdGet = connection.CreateCommand())
                    {
                        cmdGet.Transaction = transaction;
                        cmdGet.CommandText = getOrderSql;
                        cmdGet.Parameters.Add(_connectionFactory.CreateParameter("@OrderId", orderId));

                        using (DbDataReader reader = cmdGet.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                throw new InvalidOperationException($"Không tìm thấy hóa đơn ID = {orderId} để hủy!");
                            }
                            customerId = Convert.ToInt32(reader["CustomerId"]);
                            pointsToDeduct = Convert.ToInt32(reader["RewardPoints"]);
                        }
                    }

                    // 2. Lấy danh sách sản phẩm đã mua để hoàn trả kho
                    var itemsToRestore = new List<(int ProductId, int Quantity)>();
                    string getDetailsSql = "SELECT ProductId, Quantity FROM OrderDetails WHERE OrderId = @OrderId";

                    using (DbCommand cmdDetails = connection.CreateCommand())
                    {
                        cmdDetails.Transaction = transaction;
                        cmdDetails.CommandText = getDetailsSql;
                        cmdDetails.Parameters.Add(_connectionFactory.CreateParameter("@OrderId", orderId));

                        using (DbDataReader reader = cmdDetails.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                itemsToRestore.Add((
                                    Convert.ToInt32(reader["ProductId"]), 
                                    Convert.ToInt32(reader["Quantity"])
                                ));
                            }
                        }
                    }

                    // 3. Hoàn trả tồn kho trong bảng Products
                    foreach (var item in itemsToRestore)
                    {
                        string restoreStockSql = @"
                            UPDATE Products 
                            SET StockQuantity = StockQuantity + @Qty 
                            WHERE ProductId = @ProductId";

                        using (DbCommand cmdRestore = connection.CreateCommand())
                        {
                            cmdRestore.Transaction = transaction;
                            cmdRestore.CommandText = restoreStockSql;
                            cmdRestore.Parameters.Add(_connectionFactory.CreateParameter("@Qty", item.Quantity));
                            cmdRestore.Parameters.Add(_connectionFactory.CreateParameter("@ProductId", item.ProductId));
                            cmdRestore.ExecuteNonQuery();
                        }
                    }

                    // 4. Khấu trừ điểm thưởng đã tích của khách hàng
                    if (pointsToDeduct > 0 && customerId > 0)
                    {
                        string deductPointsSql = @"
                            UPDATE Customers 
                            SET RewardPoints = CASE 
                                WHEN RewardPoints >= @Pts THEN RewardPoints - @Pts 
                                ELSE 0 
                            END 
                            WHERE CustomerId = @CustomerId";

                        using (DbCommand cmdDeduct = connection.CreateCommand())
                        {
                            cmdDeduct.Transaction = transaction;
                            cmdDeduct.CommandText = deductPointsSql;
                            cmdDeduct.Parameters.Add(_connectionFactory.CreateParameter("@Pts", pointsToDeduct));
                            cmdDeduct.Parameters.Add(_connectionFactory.CreateParameter("@CustomerId", customerId));
                            cmdDeduct.ExecuteNonQuery();
                        }
                    }

                    // 5. Xóa chi tiết hóa đơn trước (tránh vi phạm ràng buộc khóa ngoại)
                    string deleteDetailsSql = "DELETE FROM OrderDetails WHERE OrderId = @OrderId";
                    using (DbCommand cmdDelDetails = connection.CreateCommand())
                    {
                        cmdDelDetails.Transaction = transaction;
                        cmdDelDetails.CommandText = deleteDetailsSql;
                        cmdDelDetails.Parameters.Add(_connectionFactory.CreateParameter("@OrderId", orderId));
                        cmdDelDetails.ExecuteNonQuery();
                    }

                    // 6. Xóa hóa đơn tổng trong bảng Orders
                    string deleteOrderSql = "DELETE FROM Orders WHERE OrderId = @OrderId";
                    using (DbCommand cmdDelOrder = connection.CreateCommand())
                    {
                        cmdDelOrder.Transaction = transaction;
                        cmdDelOrder.CommandText = deleteOrderSql;
                        cmdDelOrder.Parameters.Add(_connectionFactory.CreateParameter("@OrderId", orderId));
                        cmdDelOrder.ExecuteNonQuery();
                    }

                    // 7. Xác nhận lưu vĩnh viễn (Commit)
                    transaction.Commit();

                    return Ok(new
                    {
                        success = true,
                        message = $"Đã hủy thành công hóa đơn #{orderId} và hoàn trả toàn bộ tồn kho vào CSDL!"
                    });
                }
                catch (Exception ex)
                {
                    // Hoàn tác sạch sẽ khi có lỗi
                    try
                    {
                        transaction.Rollback();
                    }
                    catch { }

                    return BadRequest(new
                    {
                        success = false,
                        message = "Hủy hóa đơn thất bại, giao dịch đã được Rollback!",
                        errorDetail = ex.Message
                    });
                }
            }
        }
    }
}
