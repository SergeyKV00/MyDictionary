namespace MyDictionary.Api.Contracts.DictionaryItems;

public record UpdateWordRequest(
    Guid Id,
    string? Term,
    string? Meaning,
    int? Weight
);
