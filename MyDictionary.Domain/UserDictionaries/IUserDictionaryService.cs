namespace MyDictionary.Domain.UserDictionaries;

public interface IUserDictionaryService
{
    Task<bool> ExistsAsync(Guid userId, string Name, CancellationToken cancellation);
}
