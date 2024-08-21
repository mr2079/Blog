using System.Data;

namespace Article.Application.Abstractions;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}