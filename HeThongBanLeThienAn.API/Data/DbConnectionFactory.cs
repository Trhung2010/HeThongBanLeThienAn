using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
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
        public DbConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Thiếu cấu hình ConnectionStrings:DefaultConnection.");
        }

        public bool IsSqlServer => true;

        public DbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public DbDataAdapter CreateDataAdapter(DbCommand command)
        {
            if (command is SqlCommand sqlCommand) return new SqlDataAdapter(sqlCommand);
            throw new ArgumentException("Lệnh phải là SqlCommand.", nameof(command));
        }

        public DbParameter CreateParameter(string name, object? value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }
    }
}
