using AutoMapper;
using Carter;
using MediatR;

namespace Comment.Api.Endpoints;

public record CreateCommentRequest(
    object UserId,
    object ArticleId,
    string Text);

public record CreateCommentResponse();

public class CreateCommentEndpoint() : CarterModule("api/comment")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("create", async (
            CreateCommentRequest request,
            IMapper mapper,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = mapper.Map<CreateCommentCommand>(request);

            var result = await sender.Send(command, cancellationToken);

            // TODO
            return Results.Ok(result);
        });
    }
}