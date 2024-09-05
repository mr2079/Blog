using BuildingBlocks.Http;
using BuildingBlocks.Results;

namespace BuildingBlocks.Extensions;

public static class ResultExtensions
{
    public static Response ToResponse(this Result result)
        => new(result.IsSuccess, result.Error.Message);

    public static Response ToResponse<TData>(this Result<TData> result)
    {
        return result.IsSuccess 
            ? new Response<TData>(result.IsSuccess, result.Error.Message, result.Value) 
            : new Response<TData>(result.IsSuccess, result.Error.Message);
    }
}