namespace MyDictionary.Api.Contracts.DictionaryItems;

public record UpdateDictionaryItemRequest(
    Guid Id,
    string? Term,
    string? Meaning,
    int? Weight
);
