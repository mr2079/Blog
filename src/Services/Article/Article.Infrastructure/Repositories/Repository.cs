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
    public void Create(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Added;
    }

    public void Update(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(TEntity entity)
    {
        entity.Delete();
        context.Entry(entity).State = EntityState.Modified;
    }
}