using Article.Domain.Abstractions;
using Article.Domain.Categories;
using Microsoft.EntityFrameworkCore;

namespace Article.Infrastructure.Data.Context;

public sealed class ArticleDbContext(
    DbContextOptions<ArticleDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<ArticleEntity> Articles { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArticleDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}