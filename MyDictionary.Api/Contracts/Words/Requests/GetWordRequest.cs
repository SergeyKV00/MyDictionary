namespace MyDictionary.Api.Contracts.DictionaryItems.Requests;

public record GetWordRequest(
    Guid Id,
    bool IsIncludeExample = false,
    bool IsIncludeWordForm = true
);
