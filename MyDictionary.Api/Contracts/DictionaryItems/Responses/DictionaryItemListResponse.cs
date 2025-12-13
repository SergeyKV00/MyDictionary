using MyDictionary.Api.Contracts.WordProgresses.Responses;
using MyDictionary.Domain.Modules.DictionaryItems;

namespace MyDictionary.Api.Contracts.DictionaryItems;

public record DictionaryItemListResponse(
    Guid Id,
    string Term,
    string Meaning,
    int Weight,
    WordProgressResponse? WordProgress // TODO Времено
)
{
    public static DictionaryItemListResponse MapFrom(DictionaryItem model)
    {
        return new DictionaryItemListResponse(
            Id: model.Id,
            Term: model.Term,
            Meaning: model.Meaning,
            Weight: model.Weight,
            WordProgress: WordProgressResponse.Map(model.WordProgress)
        );
    }
}