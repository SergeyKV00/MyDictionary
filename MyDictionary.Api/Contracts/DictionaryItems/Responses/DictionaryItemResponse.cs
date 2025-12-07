using MyDictionary.Api.Contracts.DictionaryItemExample.Responses;
using MyDictionary.Domain.Modules.DictionaryItems;

namespace MyDictionary.Api.Contracts.DictionaryItems.Responses;

public record DictionaryItemResponse(
    Guid Id,
    string Term,
    string Meaning,
    int Weight,
    List<DictionaryItemExampleListResponse> Examples
)
{
    public static DictionaryItemResponse MapFrom(DictionaryItem model)
    {
        return new DictionaryItemResponse(
            Id: model.Id,
            Term: model.Term,
            Meaning: model.Meaning,
            Weight: model.Weight,
            Examples: [..model.Examples?.Select(DictionaryItemExampleListResponse.MapFrom) ?? []]
        );
    }
}