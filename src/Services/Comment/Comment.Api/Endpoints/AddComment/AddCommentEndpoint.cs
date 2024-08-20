using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comment.Api.Endpoints.AddComment;

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
            IMapper mapper,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<AddCommentCommand>(request);

            var result = await sender.Send(command, cancellationToken);

            var response = mapper.Map<AddCommentResponse>(result);

            return Results.Ok(response);
        });
    }
}