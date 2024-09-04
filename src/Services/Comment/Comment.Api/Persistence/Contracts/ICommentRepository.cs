using System.Linq.Expressions;
using MongoDB.Bson;

namespace Comment.Api.Persistence.Contracts;

public interface ICommentRepository
{
    Task<Result<IReadOnlyList<CommentEntity>>> GetListAsync(
        Expression<Func<CommentEntity, bool>>? predicate = null,
        int? skip = null,
        int? limit = null);

    Task<Result<CommentEntity>> GetAsync(
        Expression<Func<CommentEntity, bool>> predicate);

    Task CreateAsync(CommentEntity comment);

    Task<Result> UpdateAsync(CommentEntity comment);

    Task<Result> DeleteAsync(CommentEntity comment);

    Task<Result> DeleteAsync(Guid id);
}