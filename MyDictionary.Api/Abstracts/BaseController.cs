using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyDictionary.Domain.Common;
using System.Net;
using System.Security.Claims;

namespace MyDictionary.Api.Abstracts;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ?? HttpContext.RequestServices.GetService<IMediator>();

    internal Guid UserId => !User.Identity.IsAuthenticated
        ? Guid.Empty
        : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

    protected IActionResult HandleResult(Result result, HttpStatusCode successStatus = HttpStatusCode.OK)
    {
        var TResult = result.IsSuccess
            ? Result.Success<object>(null)
            : Result.Failure<object>(result.Error);

        return HandleResult(TResult, successStatus);
    }
 
    protected IActionResult HandleResult<T>(Result<T> result, HttpStatusCode successStatus = HttpStatusCode.OK)
    {
        if (result.IsSuccess)
        {
            if (typeof(T) == typeof(object) || result.Value == null)
                return CreateSuccessResult<object>(null, successStatus);
            return CreateSuccessResult(result.Value, successStatus);
        }

        return Problem(result);
    }

    private IActionResult CreateSuccessResult<T>(T value, HttpStatusCode status)
    {
        return status switch
        {
            HttpStatusCode.OK => Ok(value),
            HttpStatusCode.Created => Created(string.Empty, value),
            HttpStatusCode.NoContent => NoContent(),
            _ => StatusCode((int)status, value)
        };
    }

    private ObjectResult Problem(Result result)
    {
        var problem = new ProblemDetails
        {
            Status = (int)HttpStatusCode.BadRequest,
            Title = "Bad Request"
        };

        problem.Extensions["Errors"] = new[] { result.Error };
        return new ObjectResult(problem)
        {
            StatusCode = problem.Status
        };
    }
}
