using MyDictionary.Application.Common;
using MyDictionary.Domain.Common;

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

        return new ListModel<TResult>
        {
            Data = mappedList,
            Total = result.Value?.Total ?? 0
        };
    }
}
