using MyDictionary.Domain.Common;

namespace MyDictionary.Domain.Users;

public static class UserErrors
{
    public static Error NotFound(Guid userId) => 
        new("Users.NotFound", $"User with id - {userId} was not found");
}
