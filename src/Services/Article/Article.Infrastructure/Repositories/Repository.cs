using Article.Application.Abstractions;
using Article.Domain.Abstractions;
using Article.Infrastructure.Data;
using Article.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Article.Infrastructure.Repositories;

public class Repository<TEntity>(
    ArticleDbContext context)
    : IRepository<TEntity>
    where TEntity : Entity
{
    public void Create(TEntity entity)
    {
        EntityArgumentNullException<TEntity>.ThrowIfNull(entity);

        context.Entry(entity).State = EntityState.Added;
    }

    public void Update(TEntity entity)
    {
        EntityArgumentNullException<TEntity>.ThrowIfNull(entity);

        context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(TEntity entity)
    {
        EntityArgumentNullException<TEntity>.ThrowIfNull(entity);

        entity.Delete();

        context.Entry(entity).State = EntityState.Modified;
    }
}