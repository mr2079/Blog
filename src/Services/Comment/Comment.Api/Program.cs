using Carter;
using Comment.Api.Extensions;
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

builder.Services.AddScoped<ExceptionHandlingMiddleware>();

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseApplicationExceptionHandler();
}

app.UseAuthorization();

app.MapCarter();

app.Run();