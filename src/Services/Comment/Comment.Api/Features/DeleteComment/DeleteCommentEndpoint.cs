using Carter;
using Comment.Api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comment.Api.Features.DeleteComment;

public class DeleteCommentEndpoint() : CarterModule("api/v{version:apiVersion}/comments")
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
        })
        .WithApiVersionSet(ApiVersioning.ApiVersionSet)
        .MapToApiVersion(1);
    }
}