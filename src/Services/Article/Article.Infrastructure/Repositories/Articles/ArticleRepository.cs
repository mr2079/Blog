using Article.Application.Abstractions;
using Article.Application.Abstractions.Articles;
using Article.Infrastructure.Data;
using Dapper;

namespace Article.Infrastructure.Repositories.Articles;

public class ArticleRepository(
    ArticleDbContext context,
    ISqlConnectionFactory sqlConnectionFactory)
    : Repository<ArticleEntity>(context), IArticleRepository
{
    public async Task<IEnumerable<ArticleEntity>> GetArticlesByCategoryId(
        Guid categoryId,
        int offset = 0,
        int limit = 10)
    {
        using var connection = sqlConnectionFactory.CreateConnection();

        const string sql = """
                           SELECT *
                           FROM articles
                           WHERE category_id = @CategoryId
                           LIMIT @Limit
                           OFFSET @Offset
                           """;

        var articles = await connection
            .QueryAsync<ArticleEntity>(
                sql,
                new
                {
                    CategoryId = categoryId,
                    Offset = offset,
                    Limit = limit
                });

        return articles;
    }

    public async Task<IEnumerable<ArticleEntity>> GetArticlesByAuthorId(
        string authorId,
        int offset = 0,
        int limit = 10)
    {
        using var connection = sqlConnectionFactory.CreateConnection();

        const string sql = """
                           SELECT *
                           FROM articles
                           WHERE category_id = @AuthorId
                           LIMIT @Limit
                           OFFSET @Offset
                           """;

        var articles = await connection
            .QueryAsync<ArticleEntity>(
                sql,
                new
                {
                    AuthorId = authorId,
                    Offset = offset,
                    Limit = limit
                });

        return articles;
    }

    public async Task<ArticleEntity?> GetArticleById(Guid id)
    {
        using var connection = sqlConnectionFactory.CreateConnection();

        const string sql = """
                           SELECT *
                           FROM articles
                           WHERE id = @Id
                           """;

        var article = await connection
            .QuerySingleOrDefaultAsync<ArticleEntity>(
                sql,
                new
                {
                    Id = id
                });

        return article;
    }
}