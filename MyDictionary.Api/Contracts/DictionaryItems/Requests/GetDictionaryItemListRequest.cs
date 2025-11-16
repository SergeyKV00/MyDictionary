namespace MyDictionary.Api.Contracts.DictionaryItems;

public record GetDictionaryItemListRequest(
    Guid? DictionaryId,
    string? Term,
    string? Meaning
);