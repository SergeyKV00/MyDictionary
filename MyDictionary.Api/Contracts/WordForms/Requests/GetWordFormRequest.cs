namespace MyDictionary.Api.Contracts.WordForms.Requests;

public record GetWordFormRequest(
    Guid? Id,
    Guid? DictionaryItemId
);
