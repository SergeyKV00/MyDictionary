namespace MyDictionary.Api.Contracts.UserDictionaries;

using MyDictionary.Api.Contracts.DictionaryItems;
using DomainUserDictionary = Domain.Modules.UserDictionaries.UserDictionary;

public record UserDictionaryListResponse(
    Guid Id, 
    string Name,
    IReadOnlyList<WordListResponse>? Items
)
{
    public static UserDictionaryListResponse From(DomainUserDictionary model)
    {
        return new UserDictionaryListResponse(
            Id: model.Id, 
            Name: model.Name,
            Items: model.Items?.Select(WordListResponse.MapFrom).ToList()
        );
    }
}