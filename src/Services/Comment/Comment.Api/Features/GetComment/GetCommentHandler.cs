using BuildingBlocks.CQRS;
using Comment.Api.Persistence.Contracts;
using Mapster;
using MongoDB.Bson;

namespace Comment.Api.Features.GetComment;

public record GetCommentQuery(
    ObjectId Id)
    : IQuery<GetCommentResult>;

public record GetCommentResult();

public class GetCommentHandler(
    ICommentRepository commentRepository)
    : IQueryHandler<GetCommentQuery, GetCommentResult>
{
    public async Task<GetCommentResult> Handle(
        GetCommentQuery query,
        CancellationToken cancellationToken)
    {
        var comment = await commentRepository.GetAsync(
            c => c.Id == query.Id);

        var result = comment.Adapt<GetCommentResult>();

        return result;
    }
}