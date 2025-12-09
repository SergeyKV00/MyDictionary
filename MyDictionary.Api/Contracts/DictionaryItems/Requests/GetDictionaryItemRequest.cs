namespace MyDictionary.Api.Contracts.DictionaryItems.Requests;

public record GetDictionaryItemRequest(
    Guid Id,
    bool IsIncludeExample = false,
    bool IsIncludeWordForm = true
);
