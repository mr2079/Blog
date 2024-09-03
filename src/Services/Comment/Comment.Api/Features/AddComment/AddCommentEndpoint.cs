using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Comment.Api.Features.AddComment;

public record AddCommentRequest(
    object UserId,
    object ArticleId,
    string Text,
    ObjectId? ParentId = null);

public class AddCommentEndpoint() : CarterModule("api/comment")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("add", async Task<IResult> (
            [FromBody] AddCommentRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<AddCommentCommand>();

            var result =  await sender.Send(command, cancellationToken);

            var response = result.ToResponse();

            return Results.Ok(response);
        });
    }
}