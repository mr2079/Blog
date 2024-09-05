using Asp.Versioning;
using Carter;
using Comment.Api.Middlewares;
using Comment.Api.Persistence.Context;
using Comment.Api.Persistence.Contracts;
using Comment.Api.Persistence.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddCarter();

builder.Services.AddSingleton<ICommentContext, CommentContext>();

builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddExceptionHandler<ExceptionHandlingMiddleware>();

builder.Services.AddProblemDetails();

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("x-version"),
        new QueryStringApiVersionReader("v"));
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(_ => { });
}

app.UseAuthorization();

var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .HasApiVersion(new ApiVersion(2))
    .ReportApiVersions()
    .Build();

app.MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet)
    .MapCarter();

app.Run();