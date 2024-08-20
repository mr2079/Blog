using BuildingBlocks.CQRS;
using Comment.Api.Persistence.Contracts;
using CommandEntity = Comment.Api.Entities.Comment;

namespace Comment.Api.Features.Comments.AddComment;

public record AddCommentCommand(
    object UserId,
    object ArticleId,
    string Text) : ICommand<AddCommentResult>;

public record AddCommentResult();

public class EditCommentHandler(
    ICommentRepository commentRepository)
    : ICommandHandler<AddCommentCommand, AddCommentResult>
{
    public async Task<AddCommentResult> Handle(
        AddCommentCommand command,
        CancellationToken cancellationToken)
    {
        var comment = CommandEntity.Create(
            command.UserId,
            command.ArticleId,
            command.Text);

        await commentRepository.CreateAsync(comment);

        // TODO
        return new AddCommentResult();
    }
}