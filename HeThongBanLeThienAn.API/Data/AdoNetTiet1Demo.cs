using MiniSupermarket.API.Data;
using Microsoft.Extensions.Configuration;

namespace HeThongBanLeThienAn.Tiet1ConsoleDemo;

// Buổi 5, tiết 1: ExecuteScalar, ExecuteNonQuery và ExecuteReader trên SQLite.
public class AdoNetTiet1Demo
{
    private readonly IDbConnectionFactory _factory;
    public AdoNetTiet1Demo(string connectionString = "Data Source=supermarket.db")
        : this(new DbConnectionFactory(new ConfigurationBuilder().AddInMemoryCollection(
            new Dictionary<string,string?> { ["ConnectionStrings:DefaultConnection"] = connectionString }).Build())) { }
    public AdoNetTiet1Demo(IDbConnectionFactory factory) => _factory = factory;

    public int CountTotalProducts()
    {
        using var connection = _factory.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM Products";
        command.CommandTimeout = 30;
        connection.Open();
        return Convert.ToInt32(command.ExecuteScalar());
    }

    public int UpdateProductStock(int productId, int newStock)
    {
        if (productId <= 0 || newStock < 0) throw new ArgumentOutOfRangeException(nameof(newStock));
        using var connection = _factory.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "UPDATE Products SET StockQuantity=@Stock WHERE ProductId=@Id";
        command.CommandTimeout = 30;
        command.Parameters.Add(_factory.CreateParameter("@Stock", newStock));
        command.Parameters.Add(_factory.CreateParameter("@Id", productId));
        connection.Open();
        return command.ExecuteNonQuery();
    }

    public List<(int Id, string Barcode, string Name, decimal Price, int Stock)> ReadAllProducts()
    {
        using var connection = _factory.CreateConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT ProductId,Barcode,ProductName,Price,StockQuantity FROM Products ORDER BY ProductId";
        command.CommandTimeout = 30;
        connection.Open();
        using var reader = command.ExecuteReader();
        var products = new List<(int,string,string,decimal,int)>();
        while (reader.Read()) products.Add((reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
            Convert.ToDecimal(reader[3]), reader.GetInt32(4)));
        return products;
    }
}
