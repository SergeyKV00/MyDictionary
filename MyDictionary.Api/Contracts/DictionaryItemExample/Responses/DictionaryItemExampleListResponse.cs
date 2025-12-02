namespace MyDictionary.Api.Contracts.DictionaryItemExample.Responses;

public record DictionaryItemExampleListResponse(
    Guid Id,
    Guid DictionaryItemId,
    string Example,
    string? Translation
)
{
    public static DictionaryItemExampleListResponse MapFrom(
        Domain.Modules.DictionaryItemExamples.DictionaryItemExample model
    )
    {
        return new DictionaryItemExampleListResponse(
            Id: model.Id,
            DictionaryItemId: model.DictionaryItemId,
            Example: model.Example,
            Translation: model.Translation
        );
    }
}