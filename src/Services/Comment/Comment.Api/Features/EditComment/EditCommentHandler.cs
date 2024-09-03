using Comment.Api.Persistence.Contracts;
using MongoDB.Bson;

namespace Comment.Api.Features.EditComment;

public record EditCommentCommand(
    ObjectId Id,
    string Text) : ICommand<Result>;

public class DeleteCommentHandler(
    ICommentRepository commentRepository)
    : ICommandHandler<EditCommentCommand, Result>
{
    public async Task<Result> Handle(
        EditCommentCommand command,
        CancellationToken cancellationToken)
    {
        var comment = await commentRepository.GetAsync(
            c => c.Id == command.Id);

        comment.Value.Update(comment.Value.Text);

        var result = await commentRepository.UpdateAsync(comment.Value);

        return result;
    }
}