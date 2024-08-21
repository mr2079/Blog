using Article.Application.Abstractions;
using Article.Application.Abstractions.Articles;
using Article.Application.Abstractions.Categories;
using Article.Infrastructure.Data;
using Article.Infrastructure.Repositories;
using Article.Infrastructure.Repositories.Articles;
using Article.Infrastructure.Repositories.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Article.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration
            .GetConnectionString("PostgreSql");

        services.AddDbContext<ArticleDbContext>(options =>
        {
            options.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();
        });

        services.AddSingleton<ISqlConnectionFactory>(_ => 
            new SqlConnectionFactory(connectionString));

        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<ArticleDbContext>());

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IArticleRepository, ArticleRepository>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}