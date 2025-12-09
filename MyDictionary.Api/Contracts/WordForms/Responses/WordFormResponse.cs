using MyDictionary.Domain.Modules.WordForms;

namespace MyDictionary.Api.Contracts.WordForms.Responses;

public record WordFormResponse(
    Guid Id,
    string? Infinitive,
    string? PastSimple,
    string? PastParticiple
)
{
    public static WordFormResponse? Map(WordForm? model)
    {
        if (model == null) return null;

        return new WordFormResponse(
            Id: model.Id,
            Infinitive: model.Infinitive,
            PastSimple: model.PastSimple,
            PastParticiple: model.PastParticiple
        );
    }
}
