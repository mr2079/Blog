using System.Linq.Expressions;
using Comment.Api.DTOs;
using Comment.Api.Persistence.Contracts;
using Mapster;

namespace Comment.Api.Features.GetComment;

public record GetCommentListQuery(
    string? UserId = null,
    string? ArticleId = null,
    int? Skip = 0,
    int? Limit = 10)
    : IQuery<Result<IReadOnlyList<CommentDto>>>;

public class GetCommentListHandler(
    ICommentRepository commentRepository)
    : IQueryHandler<GetCommentListQuery, Result<IReadOnlyList<CommentDto>>>
{
    public async Task<Result<IReadOnlyList<CommentDto>>> Handle(
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

        var list = await commentRepository.GetListAsync(predicate, query.Skip, query.Limit);

        var value = list.Value.Adapt<IReadOnlyList<CommentDto>>();

        var result = Result.Create(value);

        return result;
    }
}