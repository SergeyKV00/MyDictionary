namespace MyDictionary.Domain.Common;

public enum SortOrders
{
    Asc = 1,
    Desc = 2,
}

public class OrderBy(string Key = "Id", SortOrders Sort = SortOrders.Asc)
{
    public string Key { get; set; } = Key;
    public SortOrders Sort { get; set; } = Sort;

}
