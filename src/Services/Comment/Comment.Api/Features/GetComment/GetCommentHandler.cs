using Comment.Api.DTOs;
using Comment.Api.Persistence.Contracts;
using Mapster;
using MongoDB.Driver;

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
        var filter = Builders<CommentEntity>.Filter.Eq(c => c.Id, query.Id);

        var comment = await commentRepository.GetAsync(filter);

        return comment.IsSuccess
            ? Result.Success(comment.Value.Adapt<CommentDto>())
            : Result.Failure<CommentDto>(comment.Error);
    }
}