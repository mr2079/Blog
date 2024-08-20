using BuildingBlocks.CQRS;
using Comment.Api.Persistence.Contracts;
using MongoDB.Bson;
using CommandEntity = Comment.Api.Entities.Comment;

namespace Comment.Api.Features.Comments.AddComment;

public record AddCommentCommand(
    object UserId,
    object ArticleId,
    string Text,
    ObjectId? ParentId = null) : ICommand<AddCommentResult>;

// TODO: fix details of result
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
            command.Text,
            command.ParentId);

        if (command.ParentId == null) // Add new comment
        {
            await commentRepository.CreateAsync(comment);
        }
        else // Add new reply
        {
            var parent = await commentRepository.GetAsync(
                c => c.Id == command.ParentId);

            parent.AddReply(comment);

            await commentRepository.UpdateAsync(parent);
        }

        return new AddCommentResult();
    }
}