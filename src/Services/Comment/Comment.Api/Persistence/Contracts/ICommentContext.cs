using MongoDB.Driver;
using CommentEntity = Comment.Api.Entities.Comment;

namespace Comment.Api.Persistence.Contracts;

public interface ICommentContext
{
    IMongoCollection<CommentEntity> Comments { get; init; }
}