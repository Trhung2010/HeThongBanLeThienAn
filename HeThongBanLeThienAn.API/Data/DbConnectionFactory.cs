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

            // Provider is explicit: a SQLite filename may contain localhost or start with ./.
            _isSqlServer = string.Equals(configuration["Database:Provider"], "SqlServer", StringComparison.OrdinalIgnoreCase);
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
            if (command is SqlCommand sqlCommand) return new SqlDataAdapter(sqlCommand);
            throw new NotSupportedException("SQLite không có DataAdapter; dùng DataTable.Load(DbDataReader).");
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
