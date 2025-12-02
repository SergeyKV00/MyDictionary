namespace MyDictionary.Api.Contracts.DictionaryItemExample.Requests;

public record CreateDictionaryItemExampleRequest(
    Guid DictionaryItemId,
    string Example,
    string? Translation
);
