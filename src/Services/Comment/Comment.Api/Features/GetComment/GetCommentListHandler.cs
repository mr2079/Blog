using System.Linq.Expressions;
using Comment.Api.Persistence.Contracts;
using Mapster;

namespace Comment.Api.Features.GetComment;

public record GetCommentListQuery(
    object? UserId = null,
    object? ArticleId = null,
    int? Skip = 0,
    int? Limit = 10)
    : IQuery<Result<IReadOnlyList<CommentEntity>>>;

public class GetCommentListHandler(
    ICommentRepository commentRepository)
    : IQueryHandler<GetCommentListQuery, Result<IReadOnlyList<CommentEntity>>>
{
    public async Task<Result<IReadOnlyList<CommentEntity>>> Handle(
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

        var result = await commentRepository.GetListAsync(predicate, query.Skip, query.Limit);

        return result;
    }
}