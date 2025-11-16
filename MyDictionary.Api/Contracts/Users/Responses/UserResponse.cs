namespace MyDictionary.Api.Contracts.Users;

public record UserResponse(
    Guid Id,
    string UserName,
    string Email,
    DateTime Created,
    DateTime? Deleted
)
{
    public static UserResponse MapFrom(Domain.Modules.Users.User model)
    {
        return new UserResponse(
            Id: model.Id,
            UserName: model.UserName,
            Email: model.Email,
            Created: model.Created,
            Deleted: model.Deleted
        );
    }
}