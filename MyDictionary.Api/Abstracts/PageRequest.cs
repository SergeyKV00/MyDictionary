using MyDictionary.Domain.Common;

namespace MyDictionary.Api.Abstracts;

public abstract record PageRequest(int Offset = 0, int Limit = int.MaxValue, OrderBy? Order = null)
{
    public int Offset { get; init; } = Offset;
    public int Limit { get; init; } = Limit;

    public OrderBy? Order {  get; init; } = Order;
}