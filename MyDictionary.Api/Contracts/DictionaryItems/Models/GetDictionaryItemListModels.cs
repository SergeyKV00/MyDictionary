namespace MyDictionary.Api.Contracts.DictionaryItems.Models;

public record GetDictionaryItemListRequest(
    Guid? DictionaryId,
    string? Term,
    string? Meaning
);

public record GetDictionaryItemListResponse(
    Guid Id,
    string Term,
    string Meaning,
    int Weight
);