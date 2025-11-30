using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;

namespace MyDictionary.Application.Common;

public class ListModel<T>
{
    public IList<T> Data { get; set; } = new List<T>();
    public int Total { get; set; } = 0;

    public ListModel(IList<T> data, int total)
    {
        Data = data;
        Total = total;
    }
}

public static class ListModelExtensions
{
    public static ListModel<T> ToListModel<T>(this IList<T> list, int? total = null)
    {
        return new ListModel<T>(list, total ?? list.Count);
    }

    public static async Task<ListModel<T>> CreateAsync<T>(this IQueryable<T> queryable,
        IQueryPages<ListModel<T>> query, CancellationToken cancellation)
    {
        var items = await queryable
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellation);

        var count = await queryable.CountAsync(cancellation);

        return new ListModel<T>(items, count);
    }
}
