using Comment.Api.DTOs;
using Comment.Api.Persistence.Contracts;
using Mapster;

namespace Comment.Api.Features.GetComment;

public record GetCommentQuery(
    Guid Id)
    : IQuery<Result<CommentDto>>;

public class GetCommentHandler(
    ICommentRepository commentRepository)
    : IQueryHandler<GetCommentQuery, Result<CommentDto>>
{
    public async Task<Result<CommentDto>> Handle(
        GetCommentQuery query,
        CancellationToken cancellationToken)
    {
        var comment = await commentRepository.GetAsync(
            c => c.Id == query.Id);

        return comment.IsSuccess
            ? Result.Success(comment.Value.Adapt<CommentDto>())
            : Result.Failure<CommentDto>(comment.Error);
    }
}