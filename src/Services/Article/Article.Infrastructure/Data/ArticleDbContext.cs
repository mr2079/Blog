using Article.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Article.Infrastructure.Data;

public sealed class ArticleDbContext(
    DbContextOptions<ArticleDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<ArticleEntity> Articles { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArticleDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}