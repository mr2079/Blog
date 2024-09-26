using Carter;
using Comment.Api.Entities;
using Comment.Api.Persistence.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Comment.Api.Features.GetComment;

public class GetCommentEndpoint() : CarterModule("comments")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        #region V1

        app.MapGet("/", async Task<IResult> (
                [FromQuery] string? userId,
                [FromQuery] string? articleId,
                [FromQuery] int? skip,
                [FromQuery] int? limit,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = new GetCommentListQuery(
                    userId,
                    articleId,
                    skip,
                    limit);

                var result = await sender.Send(query, cancellationToken);

                var response = result.ToResponse();

                return Results.Ok(response);

            }).MapToApiVersion(1);


        app.MapGet("/{id:guid}", async Task<IResult> (
                [FromRoute] Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new GetCommentQuery(id), cancellationToken);

                var response = result.ToResponse();

                return Results.Ok(response);

            }).MapToApiVersion(1);

        #endregion

        #region V2

        app.MapGet("/", async Task<IResult> (
                [FromQuery] string? userId,
                [FromQuery] string? articleId,
                [FromQuery] int? skip,
                [FromQuery] int? limit,
                ICommentRepository commentRepository) =>
            {
                Expression<Func<CommentEntity, bool>>? predicate = null;

                if (userId != null)
                {
                    predicate = predicate.And(c => c.UserId == userId);
                }

                if (articleId != null)
                {
                    predicate = predicate.And(c => c.ArticleId == articleId);
                }

                var getListResult = await commentRepository.GetListAsync(predicate, skip, limit);

                var response = getListResult.ToResponse();

                return Results.Ok(response);

            }).MapToApiVersion(2);


        app.MapGet("/{id:guid}", async Task<IResult> (
                [FromRoute] Guid id,
                ICommentRepository commentRepository) =>
        {
            var filter = Builders<CommentEntity>.Filter.Eq(c => c.Id, id);

            var getCommentResult = await commentRepository.GetAsync(filter);

            var response = getCommentResult.ToResponse();

            return Results.Ok(response);

        }).MapToApiVersion(2);

        #endregion
    }
}