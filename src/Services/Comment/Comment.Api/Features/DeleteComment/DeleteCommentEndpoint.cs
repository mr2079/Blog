using BuildingBlocks.Extensions;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Comment.Api.Features.DeleteComment;

public class DeleteCommentEndpoint() : CarterModule("api/comments")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("{id:guid}", async Task<IResult> (
            [FromRoute] Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteCommentCommand(id);

            var result = await sender.Send(command, cancellationToken);

            var response = result.ToResponse();

            return Results.Ok(response);
        });
    }
}