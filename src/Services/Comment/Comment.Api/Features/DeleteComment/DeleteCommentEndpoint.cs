using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Comment.Api.Features.DeleteComment;

public class DeleteCommentEndpoint() : CarterModule("api/comment")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("delete/{id}", async Task<IResult> (
            [FromRoute] string id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new DeleteCommentCommand(ObjectId.Parse(id));

            var result = await sender.Send(command, cancellationToken);

            var response = result.ToResponse();

            return Results.Ok(response);
        });
    }
}