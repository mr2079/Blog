using BuildingBlocks.Extensions;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Comment.Api.Features.EditComment;

public record EditCommentRequest(string Text);

public class EditCommentEndpoint() : CarterModule("api/comments")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("{id:guid}", async Task<IResult> (
            [FromRoute] Guid id,
            [FromBody] EditCommentRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new EditCommentCommand(id, request.Text);

            var result = await sender.Send(command, cancellationToken);

            var response = result.ToResponse();

            return Results.Ok(response);
        });
    }
}