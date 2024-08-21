using Article.Domain.Abstractions;

namespace Article.Application.Abstractions;

public interface IRepository<in TEntity>
    where TEntity : Entity
{
    Guid CreateAsync(TEntity entity);
    void UpdateAsync(TEntity entity);
    void DeleteAsync(TEntity entity);
}