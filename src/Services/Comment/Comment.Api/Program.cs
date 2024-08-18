using Carter;
using Comment.Api.Persistence.Context;
using Comment.Api.Persistence.Contracts;
using Comment.Api.Persistence.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddCarter();

builder.Services.AddSingleton<ICommentContext, CommentContext>();

builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapCarter();

app.Run();