using System.Linq.Expressions;
using Comment.Api.Errors;
using Comment.Api.Exceptions;
using Comment.Api.Persistence.Contracts;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Comment.Api.Persistence.Repositories;

public class CommentRepository(
    ICommentContext context) : ICommentRepository
{
    public async Task<Result<IReadOnlyList<CommentEntity>>> GetListAsync(
        Expression<Func<CommentEntity, bool>>? predicate = null,
        int? skip = 0,
        int? limit = 10)
    {
        Expression<Func<CommentEntity, bool>> where = c => !c.IsDeleted;

        if (predicate != null)
        {
            var and = Expression.AndAlso(where, predicate);
            where = Expression.Lambda<Func<CommentEntity, bool>>(and, where.Parameters.Single());
        }

        var filter = Builders<CommentEntity>.Filter.Where(where);

        var query = context.Comments.Find(filter);

        if (skip != null) query.Skip(skip);

        if (limit != null) query.Limit(limit);

        var list = await query.ToListAsync();

        return Result.Success<IReadOnlyList<CommentEntity>>(list);
    }

    public async Task<Result<CommentEntity>> GetAsync(
        Expression<Func<CommentEntity, bool>> predicate)
    {
        PredicateArgumentNullException.ThrowIfNull(predicate);

        Expression<Func<CommentEntity, bool>> where = c => !c.IsDeleted;
        var and = Expression.AndAlso(where, predicate);
        where = Expression.Lambda<Func<CommentEntity, bool>>(and, where.Parameters.Single());

        var filter = Builders<CommentEntity>.Filter.Where(where);

        var item = await context.Comments.Find(filter).SingleOrDefaultAsync();

        return item is not null
            ? Result.Success(item)
            : Result.Failure<CommentEntity>(CommentErrors.NotFound);
    }

    public async Task CreateAsync(CommentEntity comment)
    {
        CommentArgumentNullException.ThrowIfNull(comment);

        await context.Comments.InsertOneAsync(comment);
    }

    public async Task<Result> UpdateAsync(CommentEntity comment)
    {
        CommentArgumentNullException.ThrowIfNull(comment);

        var result = await context.Comments
            .ReplaceOneAsync(c => c.Id == comment.Id, comment);

        var modified =  result.IsAcknowledged && result.ModifiedCount > 0;

        return modified
            ? Result.Success()
            : Result.Failure(CommentErrors.NotUpdated);
    }

    public async Task<Result> DeleteAsync(CommentEntity comment)
    {
        CommentArgumentNullException.ThrowIfNull(comment);

        comment.Delete(true);

        var result = await context.Comments
            .ReplaceOneAsync(c => c.Id == comment.Id, comment);

        var modified = result.IsAcknowledged && result.ModifiedCount > 0;

        return modified
            ? Result.Success()
            : Result.Failure(CommentErrors.NotDeleted);
    }

    public async Task<Result> DeleteAsync(ObjectId id)
    {
        ObjectIdArgumentException.ThrowIfNull(id);

        var comment = await GetAsync(c => c.Id == id);

        return await DeleteAsync(comment.Value);
    }
}