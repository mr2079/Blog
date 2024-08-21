using System.Linq.Expressions;
using Comment.Api.Persistence.Contracts;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Comment.Api.Persistence.Repositories;

public class CommentRepository(
    ICommentContext context) : ICommentRepository
{
    public async Task<IReadOnlyList<CommentEntity>> GetListAsync(
        Expression<Func<CommentEntity,bool>>? predicate = null,
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

        return await query.ToListAsync();
    }

    public async Task<CommentEntity> GetAsync(
        Expression<Func<CommentEntity,bool>> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        Expression<Func<CommentEntity, bool>> where = c => !c.IsDeleted;
        var and = Expression.AndAlso(where, predicate);
        where = Expression.Lambda<Func<CommentEntity, bool>>(and, where.Parameters.Single());

        var filter = Builders<CommentEntity>.Filter.Where(where);

        return await context.Comments.Find(filter).SingleOrDefaultAsync();
    }

    public async Task CreateAsync(CommentEntity comment)
    {
        await context.Comments.InsertOneAsync(comment);
    }

    public async Task<bool> UpdateAsync(CommentEntity comment)
    {
        var result = await context.Comments
            .ReplaceOneAsync(c => c.Id == comment.Id, comment);

        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(CommentEntity comment)
    {
        comment.Delete(true);

        var result = await context.Comments
            .ReplaceOneAsync(c => c.Id == comment.Id, comment);

        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(ObjectId id)
    {
        var comment = await GetAsync(c => c.Id == id);
        return await DeleteAsync(comment);
    }
}