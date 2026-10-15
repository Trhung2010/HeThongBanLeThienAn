using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace HeThongBanLeThienAn.Tiet1ConsoleDemo
{
    /// <summary>
    /// Chương trình mẫu minh họa trọn vẹn 3 Bài tập thực hành của Tiết 1 theo giáo trình Buổi 5:
    ///   - Bài tập 1: Đếm tổng số mặt hàng bằng ExecuteScalar()
    ///   - Bài tập 2: Cập nhật tồn kho sản phẩm bằng ExecuteNonQuery() và SqlParameter
    ///   - Bài tập 3: Đọc danh sách sản phẩm bằng SqlDataReader
    /// </summary>
    public class AdoNetTiet1Demo
    {
        private readonly string _connectionString;

        public AdoNetTiet1Demo(string connectionString = "Server=localhost;Database=MiniSupermarketDb;Trusted_Connection=True;TrustServerCertificate=True;")
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// BÀI TẬP THỰC HÀNH 1: Đếm tổng số mặt hàng bằng ExecuteScalar()
        /// </summary>
        public int CountTotalProducts()
        {
            Console.WriteLine("=== BÀI TẬP 1: ĐẾM TỔNG SỐ LƯỢNG MẶT HÀNG ===");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM Products";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    int totalCount = Convert.ToInt32(result);
                    Console.WriteLine($"-> Tổng số mặt hàng hiện có: {totalCount} sản phẩm.");
                    return totalCount;
                }
            }
        }

        /// <summary>
        /// BÀI TẬP THỰC HÀNH 2: Cập nhật tồn kho sản phẩm bằng ExecuteNonQuery() và SqlParameter
        /// </summary>
        public int UpdateProductStock(int productId, int newStock)
        {
            Console.WriteLine("=== BÀI TẬP 2: CẬP NHẬT TỒN KHO AN TOÀN VỚI SQLPARAMETER ===");

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string updateSql = @"UPDATE Products 
                                     SET StockQuantity = @Stock 
                                     WHERE ProductId = @Id";

                using (SqlCommand command = new SqlCommand(updateSql, connection))
                {
                    command.Parameters.AddWithValue("@Stock", newStock);
                    command.Parameters.AddWithValue("@Id", productId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"-> THÀNH CÔNG: Đã cập nhật tồn kho SP ID = {productId} thành {newStock} đơn vị.");
                    }
                    else
                    {
                        Console.WriteLine($"-> CẢNH BÁO: Không tìm thấy sản phẩm có ID = {productId}.");
                    }

                    return rowsAffected;
                }
            }
        }

        /// <summary>
        /// BÀI TẬP THỰC HÀNH 3: Đọc danh sách sản phẩm bằng SqlDataReader
        /// </summary>
        public List<(int Id, string Barcode, string Name, decimal Price, int Stock)> ReadAllProducts()
        {
            Console.WriteLine("=== BÀI TẬP 3: DUYỆT DANH SÁCH BẰNG SQLDATAREADER ===");
            var list = new List<(int, string, string, decimal, int)>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity 
                                 FROM Products 
                                 ORDER BY ProductId ASC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("-----------------------------------------------------------------------------------------");
                        Console.WriteLine(string.Format("{0,-6} | {1,-14} | {2,-35} | {3,12} | {4,8}", 
                            "MÃ SP", "MÃ VẠCH", "TÊN SẢN PHẨM", "ĐƠN GIÁ", "TỒN KHO"));
                        Console.WriteLine("-----------------------------------------------------------------------------------------");

                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["ProductId"]);
                            string barcode = reader["Barcode"].ToString() ?? "";
                            string name = reader["ProductName"].ToString() ?? "";
                            decimal price = Convert.ToDecimal(reader["Price"]);
                            int stock = Convert.ToInt32(reader["StockQuantity"]);

                            list.Add((id, barcode, name, price, stock));
                            Console.WriteLine(string.Format("{0,-6} | {1,-14} | {2,-35} | {3,12:N0} đ | {4,8}", 
                                id, barcode, name, price, stock));
                        }
                        Console.WriteLine("-----------------------------------------------------------------------------------------");
                    }
                }
            }

            return list;
        }
    }
}
