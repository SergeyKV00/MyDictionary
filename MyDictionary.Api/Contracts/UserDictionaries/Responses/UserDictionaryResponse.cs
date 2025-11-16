namespace MyDictionary.Api.Contracts.UserDictionaries.Responses;
using DomainUserDictionary = Domain.Modules.UserDictionaries.UserDictionary;
public record UserDictionaryResponse(
    Guid Id,
    string Name,
    DateTime Created,
    DateTime? Deleted
)
{
    public static UserDictionaryResponse MapFrom(DomainUserDictionary model)
    {
        return new UserDictionaryResponse(
            Id: model.Id,
            Name: model.Name,
            Created: model.Created,
            Deleted: model.Deleted
        );
    }
}
