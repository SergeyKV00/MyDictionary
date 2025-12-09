using MyDictionary.Api.Interfaces;

namespace MyDictionary.Api.Contracts.DictionaryItems;

public record GetWordListRequest(
    Guid? DictionaryId,
    string? Term,
    string? Meaning,
    string? SortField,
    string? SortOrder,
    int Page,
    int PageSize
) : IPageRequest;