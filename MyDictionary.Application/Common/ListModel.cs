namespace MyDictionary.Application.Common;

public class ListModel<T>
{
    public IList<T> Data { get; set; } = new List<T>();
    public int Total { get; set; } = 0;
}

public static class ListModelExtensions
{
    public static ListModel<T> ToListModel<T>(this IList<T> list, int? total = null)
    {
        return new ListModel<T>
        {
            Data = list,
            Total = total ?? list.Count
        };
    }
}
