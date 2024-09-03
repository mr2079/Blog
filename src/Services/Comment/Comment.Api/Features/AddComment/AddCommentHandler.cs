﻿using Comment.Api.Persistence.Contracts;
using MongoDB.Bson;

namespace Comment.Api.Features.AddComment;

public record AddCommentCommand(
    object UserId,
    object ArticleId,
    string Text,
    ObjectId? ParentId = null) : ICommand<Result<AddCommentResult>>;

public record AddCommentResult(
    ObjectId Id,
    ObjectId? ParentId = null);

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

        if (command.ParentId == null)
        {
            await commentRepository.CreateAsync(comment);

            return Result.Success(new AddCommentResult(comment.Id));
        }

        var parent = await commentRepository.GetAsync(
            c => c.Id == command.ParentId);

        if (parent.IsFailure)
            return Result.Failure<AddCommentResult>(parent.Error);

        parent.Value.AddReply(comment);

        var result = await commentRepository.UpdateAsync(parent.Value);

        return result.IsSuccess
            ? Result.Success(new AddCommentResult(comment.Id, parent.Value.Id))
            : Result.Failure<AddCommentResult>(result.Error);
    }
}