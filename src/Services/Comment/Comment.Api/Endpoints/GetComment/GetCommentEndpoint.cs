using AutoMapper;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Comment.Api.Endpoints.GetComment;

public record GetCommentListRequest(
    string? UserId,
    string? ArticleId,
    int? Skip,
    int? Limit);

public record GetCommentListResponse();

public record GetCommentResponse();

public class GetCommentEndpoint() : CarterModule("api/comment")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        //app.MapGet("/", async Task<IResult> (
        //    GetCommentListRequest request,
        //    IMapper mapper,
        //    ISender sender,
        //    CancellationToken cancellationToken) =>
        //{
        //    var query = mapper.Map<GetCommentListQuery>(request);

        //    var result = await sender.Send(query, cancellationToken);

        //    var response = mapper.Map<GetCommentListResponse>(result);

        //    return Results.Ok(response);
        //});

        app.MapGet("/{id}", async Task<IResult> (
            [FromRoute] ObjectId id,
            IMapper mapper,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetCommentQuery(id), cancellationToken);

            var response = mapper.Map<GetCommentResponse>(result);

            return Results.Ok(response);
        });
    }
}