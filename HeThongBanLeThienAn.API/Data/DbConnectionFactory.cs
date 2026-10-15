using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace MiniSupermarket.API.Data
{
    public interface IDbConnectionFactory
    {
        DbConnection CreateConnection();
        DbDataAdapter CreateDataAdapter(DbCommand command);
        DbParameter CreateParameter(string name, object? value);
        bool IsSqlServer { get; }
    }

    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        private readonly bool _isSqlServer;

        public DbConnectionFactory(IConfiguration configuration)
        {
            var defaultConn = configuration.GetConnectionString("DefaultConnection") 
                ?? "Data Source=supermarket.db";
            _connectionString = defaultConn;

            // Xác định chuỗi kết nối là SQL Server hay SQLite
            _isSqlServer = defaultConn.Contains("Server=", StringComparison.OrdinalIgnoreCase) ||
                           defaultConn.Contains("Data Source=.", StringComparison.OrdinalIgnoreCase) ||
                           defaultConn.Contains("localhost", StringComparison.OrdinalIgnoreCase) ||
                           defaultConn.Contains("SQLEXPRESS", StringComparison.OrdinalIgnoreCase) ||
                           defaultConn.Contains("Trusted_Connection", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsSqlServer => _isSqlServer;

        public DbConnection CreateConnection()
        {
            if (_isSqlServer)
            {
                return new SqlConnection(_connectionString);
            }
            return new SqliteConnection(_connectionString);
        }

        public DbDataAdapter CreateDataAdapter(DbCommand command)
        {
            // Trong .NET Core, SqlDataAdapter hỗ trợ DbCommand
            // Khi dùng SQLite, SqliteDataAdapter không có sẵn trong Microsoft.Data.Sqlite
            // Nhưng ta có thể dễ dàng nạp DataTable từ DbDataReader
            return new SqlDataAdapter(command as SqlCommand ?? new SqlCommand());
        }

        public DbParameter CreateParameter(string name, object? value)
        {
            if (_isSqlServer)
            {
                return new SqlParameter(name, value ?? DBNull.Value);
            }
            return new SqliteParameter(name, value ?? DBNull.Value);
        }
    }
}
