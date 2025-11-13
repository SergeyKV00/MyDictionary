namespace MyDictionary.Api.Contracts.DictionaryItems.Models;

public record CreateDictionaryItemRequest(
    Guid DictionaryId,
    string Term,
    string Meaning
);

public record UpdateDictionaryItemRequest(
    Guid Id,
    string? Term,
    string? Meaning,
    int? Weight
);