﻿using Comment.Api.Persistence.Contracts;

namespace Comment.Api.Features.DeleteComment;

public record DeleteCommentCommand(Guid Id)
    : ICommand<Result>;

public class DeleteCommentHandler(
    ICommentRepository commentRepository)
    : ICommandHandler<DeleteCommentCommand, Result>
{
    public async Task<Result> Handle(
        DeleteCommentCommand command,
        CancellationToken cancellationToken)
    {
        var result = await commentRepository.DeleteAsync(command.Id);

        return result.IsSuccess
            ? Result.Success()
            : Result.Failure(result.Error);
    }
}