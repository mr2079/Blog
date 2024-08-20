using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Comment.Api.Features.Comments.EditComment;

public record EditCommentRequest(string Text);

public record EditCommentResponse();

public class DeleteCommentEndpoint() : CarterModule("api/comment")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("edit/{id}", async Task<IResult> (
            [FromRoute] ObjectId id,
            [FromBody] EditCommentRequest request,
            IMapper mapper,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new EditCommentCommand(id, request.Text);

            var result = await sender.Send(command, cancellationToken);

            var response = mapper.Map<EditCommentResponse>(result);

            return Results.Ok(response);
        });
    }
}