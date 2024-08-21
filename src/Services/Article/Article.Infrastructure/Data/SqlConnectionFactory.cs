using System.Data;
using Article.Application.Abstractions;
using Npgsql;

namespace Article.Infrastructure.Data;

public class SqlConnectionFactory(
    string? connectionString)
    : ISqlConnectionFactory
{
    public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        return connection;
    }
}