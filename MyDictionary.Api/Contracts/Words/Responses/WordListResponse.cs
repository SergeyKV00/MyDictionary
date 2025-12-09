using MyDictionary.Domain.Modules.DictionaryItems;

namespace MyDictionary.Api.Contracts.DictionaryItems;

public record WordListResponse(
    Guid Id,
    string Term,
    string Meaning,
    int Weight
)
{
    public static WordListResponse MapFrom(Word model)
    {
        return new WordListResponse(
            Id: model.Id,
            Term: model.Term,
            Meaning: model.Meaning,
            Weight: model.Weight
        );
    }
}