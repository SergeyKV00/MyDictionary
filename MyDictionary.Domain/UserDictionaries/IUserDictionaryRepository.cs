namespace MyDictionary.Infrastructure.Persistence.Repostiories.UserDictionaries;

public interface IUserDictionaryRepository
{
    Task<bool> ExistsAsync(Guid userId, string Name, CancellationToken cancellation);
}
