using System.Linq.Expressions;
using Comment.Api.Persistence.Contracts;

namespace Comment.Api.Features.GetComment;

public record GetCommentListQuery(
    string? UserId = null,
    string? ArticleId = null,
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
            predicate = predicate.And(c => c.UserId == query.UserId);
        }

        if (query.ArticleId != null)
        {
            predicate = predicate.And(c => c.ArticleId == query.ArticleId);
        }

        var result = await commentRepository.GetListAsync(predicate, query.Skip, query.Limit);

        return result;
    }
}