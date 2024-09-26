using MongoDB.Driver;
using System.Linq.Expressions;

namespace Comment.Api.Persistence.Contracts;

public interface ICommentRepository
{
    Task<Result<IReadOnlyList<CommentEntity>>> GetListAsync(
        FilterDefinition<CommentEntity>? filter = null,
        int? skip = null,
        int? limit = null);

    Task<Result<CommentEntity>> GetAsync(
        FilterDefinition<CommentEntity> filter);

    Task CreateAsync(CommentEntity comment);

    Task<Result> UpdateAsync(CommentEntity comment);

    Task<Result> DeleteAsync(CommentEntity comment);

    Task<Result> DeleteAsync(Guid id);
}