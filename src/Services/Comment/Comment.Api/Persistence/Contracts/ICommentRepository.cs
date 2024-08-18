using System.Linq.Expressions;
using MongoDB.Bson;
using CommentEntity = Comment.Api.Entities.Comment;

namespace Comment.Api.Persistence.Contracts;

public interface ICommentRepository
{
    Task<IReadOnlyList<CommentEntity>> GetListAsync(
        Expression<Func<CommentEntity, bool>>? predicate = null,
        int? skip = null,
        int? limit = null);

    Task<CommentEntity> GetAsync(
        Expression<Func<CommentEntity, bool>> predicate);

    Task CreateAsync(CommentEntity comment);

    Task<bool> UpdateAsync(CommentEntity comment);

    Task<bool> DeleteAsync(CommentEntity comment);

    Task<bool> DeleteAsync(ObjectId id);
}