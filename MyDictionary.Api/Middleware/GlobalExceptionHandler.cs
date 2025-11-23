using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyDictionary.Api.Extensions;
using MyDictionary.Domain.Common;
using System.Net;

namespace MyDictionary.Api.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";

        if (exception is ValidationException validationException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            var errors = validationException.Errors.Select(e => new
            {
                Property = e.PropertyName,
                Message = e.ErrorMessage
            }).ToList();

            var error = Error.Validation(
                "Validation.Error",
                "Validation failed",
                errors
            );

            var result = Result.Failure(error);       

            await httpContext.Response.WriteAsJsonAsync(result.GetProblem(), cancellationToken);
            return true;
        }

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var problemDetails = new ProblemDetails
        {
            Title = exception.Message,
            Detail = exception.StackTrace, // TODO Change on log
            Status = 500
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}