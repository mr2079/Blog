using Asp.Versioning;
using Asp.Versioning.Builder;
using Comment.Api.Middlewares;

namespace Comment.Api.Extensions;

public static class ApiVersioning
{
    public static ApiVersionSet? ApiVersionSet;
}

public static class ApplicationBuilderExtensions
{
    public static IEndpointRouteBuilder UseApiVersionSet(
        this IEndpointRouteBuilder app)
    {
        ApiVersioning.ApiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        return app;
    }
}