namespace MyDictionary.Api.Interfaces;

public interface IPageRequest
{
    public int Page {  get; init; }
    public int PageSize { get; init; }
}
