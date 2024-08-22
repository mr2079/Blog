using Article.Application.Abstractions;
using Article.Infrastructure.Data;
using Dapper;

namespace Article.Infrastructure.Repositories;

public class CategoryRepository(
    ArticleDbContext context,
    ISqlConnectionFactory sqlConnectionFactory)
    : Repository<CategoryEntity>(context), ICategoryRepository
{
    public async Task<IEnumerable<CategoryEntity>> GetCategoriesAsync()
    {
        using var connection = sqlConnectionFactory.CreateConnection();

        const string sql = """
                           SELECT *
                           FROM categories
                           """;

        var categories = await connection
            .QueryAsync<CategoryEntity>(sql);

        return categories;
    }

    public async Task<CategoryEntity?> GetCategoryByIdAsync(Guid id)
    {
        using var connection = sqlConnectionFactory.CreateConnection();

        const string sql = """
                           SELECT *
                           FROM categories
                           WHERE id = @Id
                           """;

        var category = await connection
            .QuerySingleOrDefaultAsync<CategoryEntity>(
                sql,
                new
                {
                    Id = id
                });

        return category;
    }
}