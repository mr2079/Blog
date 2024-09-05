﻿using Comment.Api.Persistence.Contracts;

namespace Comment.Api.Features.GetComment;

public record GetCommentQuery(
    Guid Id)
    : IQuery<Result<CommentEntity>>;

public class GetCommentHandler(
    ICommentRepository commentRepository)
    : IQueryHandler<GetCommentQuery, Result<CommentEntity>>
{
    public async Task<Result<CommentEntity>> Handle(
        GetCommentQuery query,
        CancellationToken cancellationToken)
    {
        var comment = await commentRepository.GetAsync(
            c => c.Id == query.Id);

        return comment.IsSuccess
            ? Result.Success(comment.Value)
            : Result.Failure<CommentEntity>(comment.Error);
    }
}