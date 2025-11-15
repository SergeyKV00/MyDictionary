using MyDictionary.Domain.Common;

namespace MyDictionary.Domain.Modules.UserDictionaries;

public static class UserDictionaryErrors
{
    public static Error NotFound(Guid id) =>
        new("Users.NotFound", $"Dictionary with id - {id} was not found");
    public static Error AlReadyExists(string Name)
        => new("UserDictionary.AlReadyExists", $"Dictionary with {Name} already exists");
}
