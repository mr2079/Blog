using Article.Domain.Abstractions;

namespace Article.Application.Abstractions;

public interface IRepository<in TEntity>
    where TEntity : Entity
{
    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}