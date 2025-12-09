namespace MyDictionary.Api.Contracts.DictionaryItems;

public record CreateWordRequest(
    Guid DictionaryId,
    string Term,
    string Meaning,
    int Weight
);