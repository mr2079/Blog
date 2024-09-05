using Comment.Api.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Comment.Api.Middlewares;

internal sealed class ExceptionHandlingMiddleware : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var exceptionDetails = GetExceptionDetails(exception);

        var response = new Response(false, exceptionDetails.Detail);

        httpContext.Response.StatusCode = StatusCodes.Status200OK;

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
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