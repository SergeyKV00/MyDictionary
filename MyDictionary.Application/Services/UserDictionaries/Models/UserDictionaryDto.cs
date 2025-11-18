using MyDictionary.Domain.Modules.Users;

namespace MyDictionary.Application.Services.UserDictionaries.Models;

public class UserDictionaryDto(
    Guid userId,
    User user,
    string name,
    DateTime created,
    DateTime? deleted
    )
{
    public Guid UserId { get; init; } = userId;
    public User User { get; init; } = user;
    public string Name { get; init; } = name;
    public DateTime Created { get; init; } = created;
    public DateTime? Deleted { get; init; } = deleted;

    public int ItemsCount { get; set; } = 0;
}
