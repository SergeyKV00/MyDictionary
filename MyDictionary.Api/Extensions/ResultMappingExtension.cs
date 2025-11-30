using Microsoft.AspNetCore.Mvc;
using MyDictionary.Application.Common;
using MyDictionary.Domain.Common;
using System.Net;

namespace MyDictionary.Api.Extensions;

public static class ResultMappingExtension
{
    public static Result<TResult> Map<TSource, TResult>(
        this Result<TSource> result,
        Func<TSource, TResult> mapper
    )
    {
        if (result.IsFailure)
            return Result.Failure<TResult>(result.Error);

        return mapper(result.Value);
    }

    public static Result<ListModel<TResult>> MapList<Tsource, TResult>(
        this Result<ListModel<Tsource>> result,
        Func<Tsource, TResult> mapper
    )
    {
        if (result.IsFailure)
            return Result.Failure<ListModel<TResult>>(result.Error);

        var sourceList = result.Value?.Data ?? new List<Tsource>();
        var mappedList = sourceList.Select(mapper).ToList();

        return mappedList.ToListModel(result.Value?.Total ?? 0);
    }

    public static ProblemDetails GetProblem(this Result result)
    {
        var problem = new ProblemDetails
        {
            Status = (int)HttpStatusCode.BadRequest,
            Title = "Bad Request"
        };

        problem.Extensions["isSuccess"] = false;
        problem.Extensions["error"] = result.Error;

        return problem;
    }
}
