using BuildingBlocks.Results;

namespace BuildingBlocks.Http;

public record Response(
    bool Success,
    string? Message = null)
{
    public static implicit operator Response(Result result) 
        => new(result.IsSuccess, result.Error.Message);
}

public record Response<TData>(
    bool Success,
    string? Message = null,
    TData? Data = default) : Response(Success, Message)
{
    public static implicit operator Response<TData>(Result<TData> result) 
        => new(
            result.IsSuccess,
            result.Error.Message,
            result.Value);
}