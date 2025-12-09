using MyDictionary.Domain.Common;

namespace MyDictionary.Api.Contracts.WordForms.Requests;

public record UpdateWordFormRequest(
    Guid Id,
    Optional<string?> Infinitive,
    Optional<string?> PastSimple,
    Optional<string?> PastParticiple
);
