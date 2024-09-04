using Comment.Api.Middlewares;

namespace Comment.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseApplicationExceptionHandler(
        this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        return app;
    }
}