using MyDictionary.Api.Contracts.DictionaryItems.Models;
using MyDictionary.Domain.UserDictionaryItems;

namespace MyDictionary.Api.Contracts.DictionaryItems;

public static class DictionaryItemMapping
{
    public static GetDictionaryItemListResponse ToResponse(this DictionaryItem model)
    {
        return new GetDictionaryItemListResponse(
            Id: model.Id,
            Term: model.Term,
            Meaning: model.Meaning,
            Weight: model.Weight
        );
    }
}
