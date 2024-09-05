using Carter;
using Comment.Api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comment.Api.Features.GetComment;

public class GetCommentEndpoint() : CarterModule("api/v{version:apiVersion}/comments")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async Task<IResult> (
            [FromQuery] string? userId,
            [FromQuery] string? articleId,
            [FromQuery] int? skip,
            [FromQuery] int? limit,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCommentListQuery(
                userId,
                articleId,
                skip,
                limit);

            var result = await sender.Send(query, cancellationToken);

            var response = result.ToResponse();

            return Results.Ok(response);
        })
        .WithApiVersionSet(ApiVersioning.ApiVersionSet)
        .MapToApiVersion(1);

        app.MapGet("/{id:guid}", async Task<IResult> (
            [FromRoute] Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetCommentQuery(id), cancellationToken);

            var response = result.ToResponse();

            return Results.Ok(response);
        })
        .WithApiVersionSet(ApiVersioning.ApiVersionSet)
        .MapToApiVersion(1);
    }
}