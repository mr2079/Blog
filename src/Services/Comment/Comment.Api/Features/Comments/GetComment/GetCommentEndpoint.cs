using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Comment.Api.Features.Comments.GetComment;

public record GetCommentListResponse();

public record GetCommentResponse();

public class GetCommentEndpoint() : CarterModule("api/comment")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async Task<IResult> (
            [FromQuery] string? userId,
            [FromQuery] string? articleId,
            [FromQuery] int? skip,
            [FromQuery] int? limit,
            IMapper mapper,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetCommentListQuery(
                userId,
                articleId,
                skip,
                limit);

            var result = await sender.Send(query, cancellationToken);

            var response = mapper.Map<GetCommentListResponse>(result);

            return Results.Ok(response);
        });

        app.MapGet("/{id}", async Task<IResult> (
            [FromRoute] string id,
            IMapper mapper,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetCommentQuery(ObjectId.Parse(id)), cancellationToken);

            var response = mapper.Map<GetCommentResponse>(result);

            return Results.Ok(response);
        });
    }
}