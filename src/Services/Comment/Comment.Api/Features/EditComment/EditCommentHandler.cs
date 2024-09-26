using Comment.Api.Persistence.Contracts;
using MongoDB.Driver;

namespace Comment.Api.Features.EditComment;

public record EditCommentCommand(
    Guid Id,
    string Text) : ICommand<Result>;

public class DeleteCommentHandler(
    ICommentRepository commentRepository)
    : ICommandHandler<EditCommentCommand, Result>
{
    public async Task<Result> Handle(
        EditCommentCommand command,
        CancellationToken cancellationToken)
    {
        var filter = Builders<CommentEntity>.Filter.Eq(c => c.Id, command.Id);

        var comment = await commentRepository.GetAsync(filter);

        comment.Value.Update(command.Text);

        var result = await commentRepository.UpdateAsync(comment.Value);

        return result;
    }
}