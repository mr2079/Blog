using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Comment.Api.Features.GetComment;

public class GetCommentEndpoint() : CarterModule("api/comment")
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
        });

        app.MapGet("/{id}", async Task<IResult> (
            [FromRoute] string id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetCommentQuery(ObjectId.Parse(id)), cancellationToken);

            var response = result.ToResponse();

            return Results.Ok(response);
        });
    }
}