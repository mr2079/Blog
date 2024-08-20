using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comment.Api.Features.Comments.AddComment;

public record AddCommentRequest(
    object UserId,
    object ArticleId,
    string Text);

public record AddCommentResponse();

public class EditCommentEndpoint() : CarterModule("api/comment")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("add", async Task<IResult> (
            [FromBody] AddCommentRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<AddCommentCommand>();

            var result = await sender.Send(command, cancellationToken);

            var response = result.Adapt<AddCommentResponse>();

            return Results.Ok(response);
        });
    }
}