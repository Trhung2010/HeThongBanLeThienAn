using System.Data.Common;
using Microsoft.Data.Sqlite;
using MiniSupermarket.API.Data;

namespace HeThongBanLeThienAn.Tests;

internal sealed class SqliteTestDbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SqliteTestDbConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public bool IsSqlServer => false;
    public DbConnection CreateConnection() => new SqliteConnection(_connectionString);
    public DbDataAdapter CreateDataAdapter(DbCommand command) => throw new NotSupportedException();
    public DbParameter CreateParameter(string name, object? value) =>
        new SqliteParameter(name, value ?? DBNull.Value);
}
