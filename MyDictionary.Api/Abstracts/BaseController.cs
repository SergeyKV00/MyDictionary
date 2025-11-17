using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyDictionary.Api.Extensions;
using MyDictionary.Application.Common;
using MyDictionary.Domain.Common;
using System.Net;

namespace MyDictionary.Api.Abstracts;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ?? HttpContext.RequestServices.GetService<IMediator>();

    protected async Task<IActionResult> Send(
    IRequest<Result> query,
    HttpStatusCode success = HttpStatusCode.OK)
    {
        var result = await Mediator.Send(query);
        return HandleResult(result, success);
    }

    protected async Task<IActionResult> Send<TSource>(
        IRequest<Result<TSource>> query,
        HttpStatusCode success = HttpStatusCode.OK)
    {
        return await Send<TSource, TSource>(query, null, success);
    }

    protected async Task<IActionResult> Send<TSource, TResult>(
        IRequest<Result<TSource>> query,
        Func<TSource, TResult>? mapper = null,
        HttpStatusCode success = HttpStatusCode.OK)
    {
        var result = await Mediator.Send(query);

        if (mapper is null)
            return HandleResult(result, success);

        return HandleResult(result.Map(mapper), success);
    }

    protected async Task<IActionResult> Send<TSource, TResult>(
        IRequest<Result<ListModel<TSource>>> query,
        Func<TSource, TResult>? mapper = null,
        HttpStatusCode success = HttpStatusCode.OK)
    {
        var result = await Mediator.Send(query);

        if (mapper is null)
            return HandleResult(result, success);

        return HandleResult(result.MapList(mapper), success);
    }

    protected IActionResult HandleResult(Result result, 
        HttpStatusCode successStatus = HttpStatusCode.OK)
    {
        var TResult = result.IsSuccess
            ? Result.Success<string?>(null)
            : Result.Failure<string?>(result.Error);

        return HandleResult(TResult, successStatus);
    }
 
    protected IActionResult HandleResult<T>(Result<T> result, 
        HttpStatusCode successStatus = HttpStatusCode.OK)
    {
        if (result.IsSuccess)
        {
            var sussessResponse = new SuccessResponse<T>(
                Value: result.Value,
                IsSuccess: result.IsSuccess
            );
            return CreateSuccessResult(sussessResponse, successStatus);
        }

        return new ObjectResult(result.GetProblem());
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
}
