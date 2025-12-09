namespace MyDictionary.Api.Contracts.WordForms.Requests;

public record CreateWordFormRequest(
    Guid DictionaryItemId,
    string? Infinitive,
    string? PastSimple,
    string? PastParticiple
);
