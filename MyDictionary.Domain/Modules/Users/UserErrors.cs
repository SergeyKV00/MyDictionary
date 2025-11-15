using MyDictionary.Domain.Common;

namespace MyDictionary.Domain.Modules.Users;

public static class UserErrors
{
    public static Error NotFound(Guid userId) =>
        new("Users.NotFound", $"User with id - '{userId}' was not found.");
    public static Error UserNameAlreadyTaken(string userName) =>
        new("Users.UserNameAlreadyTaken", $"Username '{userName}' is already taken.");
    public static Error EmailAlreadyUse(string email) =>
        new("Users.EmailAlreadyUse", $"Email '{email}' is already use.");
    public static Error InvalidCredentials() =>
        new("User.InvalidCredentials", $"Incorrect login or password.");
}
