namespace Article.Application.Abstractions;

public interface ICategoryRepository
    : IRepository<CategoryEntity>
{
    Task<IEnumerable<CategoryEntity>> GetCategoriesAsync();

    Task<CategoryEntity?> GetCategoryByIdAsync(Guid id);
}