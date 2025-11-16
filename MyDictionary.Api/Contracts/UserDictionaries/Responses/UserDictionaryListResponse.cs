namespace MyDictionary.Api.Contracts.UserDictionaries;
using DomainUserDictionary = Domain.Modules.UserDictionaries.UserDictionary;

public record UserDictionaryListResponse(Guid Id, string Name)
{
    public static UserDictionaryListResponse From(DomainUserDictionary model)
    {
        return new UserDictionaryListResponse(Id: model.Id, Name: model.Name);
    }
}