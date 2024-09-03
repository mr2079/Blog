using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Comment.Api.Features.EditComment;

public record EditCommentRequest(string Text);

public class EditCommentEndpoint() : CarterModule("api/comment")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("edit/{id}", async Task<IResult> (
            [FromRoute] string id,
            [FromBody] EditCommentRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new EditCommentCommand(ObjectId.Parse(id), request.Text);

            var result = await sender.Send(command, cancellationToken);

            var response = result.ToResponse();

            return Results.Ok(response);
        });
    }
}