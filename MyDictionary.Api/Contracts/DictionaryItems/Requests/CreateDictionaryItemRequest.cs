namespace MyDictionary.Api.Contracts.DictionaryItems;

public record CreateDictionaryItemRequest(
    Guid DictionaryId,
    string Term,
    string Meaning,
    int Weight
);