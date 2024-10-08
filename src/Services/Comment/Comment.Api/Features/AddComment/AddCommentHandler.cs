﻿using Comment.Api.Persistence.Contracts;
using MongoDB.Driver;

namespace Comment.Api.Features.AddComment;

public record AddCommentCommand(
    string UserId,
    string ArticleId,
    string Text,
    Guid? ParentId = null) : ICommand<Result<AddCommentResult>>;

public record AddCommentResult(
    Guid Id,
    Guid? ParentId = null);

public class AddCommentHandler(
    ICommentRepository commentRepository)
    : ICommandHandler<AddCommentCommand, Result<AddCommentResult>>
{
    public async Task<Result<AddCommentResult>> Handle(
        AddCommentCommand command,
        CancellationToken cancellationToken)
    {
        var comment = CommentEntity.Create(
            command.UserId,
            command.ArticleId,
            command.Text,
            command.ParentId);

        await commentRepository.CreateAsync(comment);

        if (comment.ParentId == null)
            return Result.Success(new AddCommentResult(comment.Id));

        var filter = Builders<CommentEntity>.Filter.Eq(c => c.Id, comment.ParentId);

        var parent = await commentRepository.GetAsync(filter);

        if (parent.IsFailure)
            return Result.Failure<AddCommentResult>(parent.Error);

        parent.Value.AddReply(comment.Id);

        var result = await commentRepository.UpdateAsync(parent.Value);

        return result.IsSuccess
            ? Result.Success(new AddCommentResult(comment.Id, parent.Value.Id))
            : Result.Failure<AddCommentResult>(result.Error);
    }
}