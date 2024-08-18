using AutoMapper;
using BuildingBlocks.CQRS;
using Comment.Api.Persistence.Contracts;
using MongoDB.Bson;

namespace Comment.Api.Endpoints.GetComment;

public record GetCommentQuery(
    ObjectId Id)
    : IQuery<GetCommentResult>;

public record GetCommentResult();

public class GetCommentHandler(
    ICommentRepository commentRepository,
    IMapper mapper)
    : IQueryHandler<GetCommentQuery, GetCommentResult>
{
    public async Task<GetCommentResult> Handle(
        GetCommentQuery query,
        CancellationToken cancellationToken)
    {
        var comment = await commentRepository.GetAsync(
            c => c.Id == query.Id);

        var result = mapper.Map<GetCommentResult>(comment);

        return result;
    }
}