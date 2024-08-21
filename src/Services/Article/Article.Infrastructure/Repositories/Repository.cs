using Article.Application.Abstractions;
using Article.Domain.Abstractions;
using Article.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Article.Infrastructure.Repositories;

public class Repository<TEntity>(
    ArticleDbContext context)
    : IRepository<TEntity>
    where TEntity : Entity
{
    public Guid CreateAsync(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Added;
        return entity.Id;
    }

    public void UpdateAsync(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteAsync(TEntity entity)
    {
        entity.Delete(true);
        context.Entry(entity).State = EntityState.Modified;
    }
}