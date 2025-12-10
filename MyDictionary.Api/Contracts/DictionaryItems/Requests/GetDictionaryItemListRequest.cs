using MyDictionary.Api.Interfaces;

namespace MyDictionary.Api.Contracts.DictionaryItems;

public record GetDictionaryItemListRequest(
    Guid? DictionaryId,
    string? Term,
    string? Meaning,
    string? SortField,
    string? SortOrder,
    int Page,
    int PageSize,
    bool IsIncludeWordProgress = false
) : IPageRequest;