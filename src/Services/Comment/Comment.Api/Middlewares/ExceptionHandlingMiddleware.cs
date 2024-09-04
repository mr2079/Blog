using Comment.Api.Exceptions;

namespace Comment.Api.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var exceptionDetails = GetExceptionDetails(exception);

            var response = new Response(false, exceptionDetails.Detail);

            context.Response.StatusCode = StatusCodes.Status200OK;

            await context.Response.WriteAsJsonAsync(response);
        }
    }

    private static ExceptionDetails GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            PredicateArgumentNullException predicateArgumentNullException => new(
                StatusCodes.Status400BadRequest,
                "ArgumentError",
                predicateArgumentNullException.ParamName!,
                predicateArgumentNullException.Message),

            CommentArgumentNullException commentArgumentNullException => new(
                StatusCodes.Status400BadRequest,
                "ArgumentError",
                commentArgumentNullException.ParamName!,
                commentArgumentNullException.Message),

            GuidArgumentException guidArgumentException => new(
                StatusCodes.Status400BadRequest,
                "ArgumentError",
                guidArgumentException.ParamName!,
                guidArgumentException.Message),

            _ => new(
                StatusCodes.Status500InternalServerError,
                "ServerError",
                "Server error",
                "An unexpected error has occurred")
        };
    }

    internal record ExceptionDetails(
        int StatusCode,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object>? Errors = null);
}