using Comment.Api.Persistence.Context;
using Comment.Api.Persistence.Contracts;
using Comment.Api.Persistence.Repositories;

namespace Comment.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

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

        app.Run();
    }
}