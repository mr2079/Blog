using BuildingBlocks.Http;
using BuildingBlocks.Results;

namespace BuildingBlocks;

public static class Extensions
{
    public static Response ToResponse(this Result result)
        => new Response(result.IsSuccess, result.Error.Message);

    public static Response ToResponse<TData>(this Result<TData> result)
        => new Response<TData>(result.IsSuccess, result.Error.Message, result.Value);
}