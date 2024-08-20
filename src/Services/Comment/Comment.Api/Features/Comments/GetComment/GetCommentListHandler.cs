using System.Linq.Expressions;
using BuildingBlocks.CQRS;
using Comment.Api.Persistence.Contracts;
using Mapster;
using CommentEntity = Comment.Api.Entities.Comment;

namespace Comment.Api.Features.Comments.GetComment;

public record GetCommentListQuery(
    object? UserId = null,
    object? ArticleId = null,
    int? Skip = 0,
    int? Limit = 10)
    : IQuery<GetCommentListResult>;

public record GetCommentListResult();

public class GetCommentListHandler(
    ICommentRepository commentRepository)
    : IQueryHandler<GetCommentListQuery, GetCommentListResult>
{
    public async Task<GetCommentListResult> Handle(
        GetCommentListQuery query,
        CancellationToken cancellationToken)
    {
        Expression<Func<CommentEntity, bool>>? predicate = null;

        if (query.UserId != null)
        {
            Expression<Func<CommentEntity, bool>> condition =
                c => c.UserId == query.UserId.ToString();

            var and = Expression.AndAlso(predicate!, condition);

            predicate = Expression.Lambda<Func<CommentEntity, bool>>(and, predicate!.Parameters.Single());
        }

        if (query.ArticleId != null)
        {
            Expression<Func<CommentEntity, bool>> condition =
                c => c.ArticleId == query.ArticleId.ToString();

            var and = Expression.AndAlso(predicate!, condition);

            predicate = Expression.Lambda<Func<CommentEntity, bool>>(and, predicate!.Parameters.Single());
        }

        var list = await commentRepository.GetListAsync(predicate, query.Skip, query.Limit);

        var result = list.Adapt<GetCommentListResult>();

        return result;
    }
}