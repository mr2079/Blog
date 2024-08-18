using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Comment.Api.Endpoints.CreateComment;

public record CreateCommentRequest(
    object UserId,
    object ArticleId,
    string Text);

public record CreateCommentResponse();

public class CreateCommentEndpoint() : CarterModule("api/comment")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("create", async Task<IResult> (
            [FromBody] CreateCommentRequest request,
            IMapper mapper,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<CreateCommentCommand>(request);

            var result = await sender.Send(command, cancellationToken);

            var response = mapper.Map<CreateCommentResponse>(result);

            return Results.Ok(response);
        });
    }
}