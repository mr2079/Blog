using MongoDB.Driver;

namespace Comment.Api.Persistence.Contracts;

public interface ICommentContext
{
    IMongoCollection<CommentEntity> Comments { get; init; }
}