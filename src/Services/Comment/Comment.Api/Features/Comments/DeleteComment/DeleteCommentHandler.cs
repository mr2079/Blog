using BuildingBlocks.CQRS;
using Comment.Api.Persistence.Contracts;
using MongoDB.Bson;

namespace Comment.Api.Features.Comments.DeleteComment;

public record DeleteCommentCommand(ObjectId Id)
    : ICommand<DeleteCommentResult>;

public record DeleteCommentResult();

public class DeleteCommentHandler(
    ICommentRepository commentRepository)
    : ICommandHandler<DeleteCommentCommand, DeleteCommentResult>
{
    public async Task<DeleteCommentResult> Handle(
        DeleteCommentCommand command,
        CancellationToken cancellationToken)
    {
        await commentRepository.DeleteAsync(command.Id);

        // TODO
        return new DeleteCommentResult();
    }
}