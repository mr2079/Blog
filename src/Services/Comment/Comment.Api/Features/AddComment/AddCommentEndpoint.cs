using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comment.Api.Features.AddComment;

public record AddCommentRequest(
    string UserId,
    string ArticleId,
    string Text,
    Guid? ParentId = null);

public class AddCommentEndpoint() : CarterModule("comments")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async Task<IResult> (
            [FromBody] AddCommentRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<AddCommentCommand>();

            var result =  await sender.Send(command, cancellationToken);

            var response = result.ToResponse();

            return Results.Ok(response);
        })
        .MapToApiVersion(1);
    }
}