namespace Article.Application.Abstractions.Articles;

public interface IArticleRepository
    : IRepository<ArticleEntity>
{
    Task<IEnumerable<ArticleEntity>> GetArticlesByCategoryIdAsync(
        Guid categoryId,
        int offset = 0,
        int limit = 10);

    Task<IEnumerable<ArticleEntity>> GetArticlesByAuthorIdAsync(
        string authorId,
        int offset = 0,
        int limit = 10);

    Task<ArticleEntity?> GetArticleByIdAsync(Guid id);
}