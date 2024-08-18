using BuildingBlocks.CQRS;
using Comment.Api.Persistence.Contracts;
using CommandEntity = Comment.Api.Entities.Comment;

namespace Comment.Api.Endpoints;

public record CreateCommentCommand(
    object UserId,
    object ArticleId,
    string Text) : ICommand<CreateCommentResult>;

public record CreateCommentResult();

public class CreateCommentHandler(
    ICommentRepository commentRepository)
    : ICommandHandler<CreateCommentCommand, CreateCommentResult>
{
    public async Task<CreateCommentResult> Handle(
        CreateCommentCommand command,
        CancellationToken cancellationToken)
    {
        var comment = CommandEntity.Create(
            command.UserId,
            command.ArticleId,
            command.Text);

        await commentRepository.CreateAsync(comment);

        // TODO
        return new CreateCommentResult();
    }
}