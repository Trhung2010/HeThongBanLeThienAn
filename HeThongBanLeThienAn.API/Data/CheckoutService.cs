using System.Data.Common;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data;

// Both HTTP checkout routes use this single ADO.NET transaction implementation.
public sealed class CheckoutService(IDbConnectionFactory factory)
{
    private DbCommand Command(DbConnection connection, DbTransaction transaction, string sql,
        params (string Name, object? Value)[] parameters)
    {
        var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = sql;
        foreach (var p in parameters) command.Parameters.Add(factory.CreateParameter(p.Name, p.Value));
        return command;
    }

    public CheckoutReceipt Checkout(string cashier, int? customerId, string? phone, List<CartItemCheckoutDto> items)
    {
        if (items == null || items.Count == 0 || items.Any(i => i == null || i.ProductId <= 0 || i.Quantity <= 0))
            throw new InvalidOperationException("Giỏ hàng phải có sản phẩm và số lượng dương.");
        if (string.IsNullOrWhiteSpace(cashier)) throw new InvalidOperationException("Thiếu tài khoản thu ngân.");
        using var connection = factory.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();
        try
        {
            using (var user = Command(connection, transaction,
                "SELECT COUNT(*) FROM Users WHERE Username = @Name AND IsActive = 1 AND Role IN ('Admin', 'Cashier')", ("@Name", cashier)))
                if (Convert.ToInt32(user.ExecuteScalar()) != 1) throw new InvalidOperationException("Tài khoản không có quyền bán hàng.");

            string customerName = "Khách vãng lai";
            if (!string.IsNullOrWhiteSpace(phone))
            {
                using var lookup = Command(connection, transaction, "SELECT CustomerId FROM Customers WHERE PhoneNumber = @Phone", ("@Phone", phone.Trim()));
                var found = lookup.ExecuteScalar();
                if (found == null) throw new InvalidOperationException("Số điện thoại chưa được đăng ký. Hãy đăng ký hoặc để trống để bán khách vãng lai.");
                customerId = Convert.ToInt32(found);
            }
            if (customerId.HasValue)
            {
                using var customer = Command(connection, transaction, "SELECT CustomerName FROM Customers WHERE CustomerId = @Id", ("@Id", customerId));
                customerName = customer.ExecuteScalar()?.ToString() ?? throw new InvalidOperationException("Không tìm thấy khách hàng.");
            }

            // Merge duplicate product lines before stock validation; ignore client-supplied prices.
            var lines = new List<(int ProductId, int Quantity, decimal Price)>();
            foreach (var group in items.GroupBy(i => i.ProductId).OrderBy(g => g.Key))
            {
                int quantity = group.Sum(i => i.Quantity);
                using var product = Command(connection, transaction, "SELECT Price, StockQuantity FROM Products WHERE ProductId = @Id", ("@Id", group.Key));
                using var reader = product.ExecuteReader();
                if (!reader.Read()) throw new InvalidOperationException($"Không tìm thấy sản phẩm {group.Key}.");
                decimal price = Convert.ToDecimal(reader["Price"]);
                if (price < 0 || Convert.ToInt32(reader["StockQuantity"]) < quantity)
                    throw new InvalidOperationException($"Sản phẩm {group.Key} không đủ tồn kho hoặc giá không hợp lệ.");
                lines.Add((group.Key, quantity, price));
            }
            decimal total = lines.Sum(i => i.Price * i.Quantity);
            int count = lines.Sum(i => i.Quantity);
            int points = customerId.HasValue ? checked((int)(total / 10000m)) : 0;
            string code = $"HD-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid():N}";
            DateTime created = DateTime.Now;
            string insert = factory.IsSqlServer
                ? "INSERT INTO Orders (OrderCode, CreatedDate, CashierUsername, CustomerId, TotalItems, FinalAmount, RewardPoints) OUTPUT INSERTED.OrderId VALUES (@Code,@Date,@Cashier,@Customer,@Count,@Total,@Points)"
                : "INSERT INTO Orders (OrderCode, CreatedDate, CashierUsername, CustomerId, TotalItems, FinalAmount, RewardPoints) VALUES (@Code,@Date,@Cashier,@Customer,@Count,@Total,@Points); SELECT last_insert_rowid();";
            int id;
            using (var order = Command(connection, transaction, insert, ("@Code", code), ("@Date", created), ("@Cashier", cashier), ("@Customer", customerId), ("@Count", count), ("@Total", total), ("@Points", points)))
                id = Convert.ToInt32(order.ExecuteScalar());
            foreach (var line in lines)
            {
                using var stock = Command(connection, transaction,
                    "UPDATE Products SET StockQuantity = StockQuantity - @Qty WHERE ProductId = @Id AND StockQuantity >= @Qty",
                    ("@Qty", line.Quantity), ("@Id", line.ProductId));
                if (stock.ExecuteNonQuery() != 1) throw new InvalidOperationException("Tồn kho đã thay đổi, vui lòng thử lại.");
                using var detail = Command(connection, transaction,
                    "INSERT INTO OrderDetails (OrderId,ProductId,Quantity,UnitPrice,LineTotal) VALUES (@Order,@Product,@Qty,@Price,@Total)",
                    ("@Order", id), ("@Product", line.ProductId), ("@Qty", line.Quantity), ("@Price", line.Price), ("@Total", line.Price * line.Quantity));
                detail.ExecuteNonQuery();
            }
            if (customerId.HasValue)
            {
                using var update = Command(connection, transaction,
                    "UPDATE Customers SET RewardPoints = RewardPoints + @Points, MembershipRank = CASE WHEN RewardPoints + @Points >= 1000 THEN 'Bạch Kim' WHEN RewardPoints + @Points >= 500 THEN 'Vàng' WHEN RewardPoints + @Points >= 200 THEN 'Bạc' ELSE 'Chuẩn' END WHERE CustomerId = @Id",
                    ("@Points", points), ("@Id", customerId));
                if (update.ExecuteNonQuery() != 1) throw new InvalidOperationException("Không cập nhật được khách hàng.");
            }
            transaction.Commit();
            return new(id, code, count, total, points, cashier, customerName, created);
        }
        catch { transaction.Rollback(); throw; }
    }

    public void Cancel(int orderId)
    {
        using var connection = factory.CreateConnection();
        connection.Open();
        using var transaction = connection.BeginTransaction();
        try
        {
            int? customerId; int points;
            using (var order = Command(connection, transaction, "SELECT CustomerId,RewardPoints FROM Orders WHERE OrderId=@Id", ("@Id", orderId)))
            using (var reader = order.ExecuteReader())
            {
                if (!reader.Read()) throw new InvalidOperationException("Không tìm thấy hóa đơn để hủy.");
                customerId = reader.IsDBNull(0) ? null : reader.GetInt32(0);
                points = reader.GetInt32(1);
            }
            var lines = new List<(int Id, int Qty)>();
            using (var details = Command(connection, transaction, "SELECT ProductId,Quantity FROM OrderDetails WHERE OrderId=@Id", ("@Id", orderId)))
            using (var reader = details.ExecuteReader())
                while (reader.Read()) lines.Add((reader.GetInt32(0), reader.GetInt32(1)));
            foreach (var line in lines)
            {
                using var restore = Command(connection, transaction, "UPDATE Products SET StockQuantity=StockQuantity+@Qty WHERE ProductId=@Id", ("@Qty", line.Qty), ("@Id", line.Id));
                if (restore.ExecuteNonQuery() != 1) throw new InvalidOperationException("Không hoàn trả được tồn kho.");
            }
            if (customerId.HasValue)
            {
                using var customer = Command(connection, transaction,
                    "UPDATE Customers SET RewardPoints=CASE WHEN RewardPoints >= @Points THEN RewardPoints-@Points ELSE 0 END, MembershipRank=CASE WHEN RewardPoints-@Points >= 1000 THEN 'Bạch Kim' WHEN RewardPoints-@Points >= 500 THEN 'Vàng' WHEN RewardPoints-@Points >= 200 THEN 'Bạc' ELSE 'Chuẩn' END WHERE CustomerId=@Id", ("@Points", points), ("@Id", customerId));
                if (customer.ExecuteNonQuery() != 1) throw new InvalidOperationException("Không hoàn trả được điểm khách hàng.");
            }
            using (var details = Command(connection, transaction, "DELETE FROM OrderDetails WHERE OrderId=@Id", ("@Id", orderId))) details.ExecuteNonQuery();
            using (var order = Command(connection, transaction, "DELETE FROM Orders WHERE OrderId=@Id", ("@Id", orderId)))
                if (order.ExecuteNonQuery() != 1) throw new InvalidOperationException("Hóa đơn đã được hủy.");
            transaction.Commit();
        }
        catch { transaction.Rollback(); throw; }
    }
}
public record CheckoutReceipt(int OrderId, string OrderCode, int TotalItems, decimal FinalAmount,
    int RewardPointsAdded, string Cashier, string Customer, DateTime Timestamp);
