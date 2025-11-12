using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MyDictionary.Api.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception
        , CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Title = exception.Message
        };

        problemDetails.Status = httpContext.Response.StatusCode;
        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken)
            .ConfigureAwait(false);

        return true;
    }
}
