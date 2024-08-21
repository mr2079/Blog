using BuildingBlocks.CQRS;
using Comment.Api.Persistence.Contracts;
using MongoDB.Bson;

namespace Comment.Api.Features.EditComment;

public record EditCommentCommand(
    ObjectId Id,
    string Text) : ICommand<EditCommentResult>;

public record EditCommentResult();

public class DeleteCommentHandler(
    ICommentRepository commentRepository)
    : ICommandHandler<EditCommentCommand, EditCommentResult>
{
    public async Task<EditCommentResult> Handle(
        EditCommentCommand command,
        CancellationToken cancellationToken)
    {
        var comment = await commentRepository.GetAsync(
            c => c.Id == command.Id);

        comment.Update(comment.Text);

        await commentRepository.UpdateAsync(comment);

        // TODO
        return new EditCommentResult();
    }
}