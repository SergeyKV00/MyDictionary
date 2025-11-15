namespace MyDictionary.Domain.Modules.UserDictionaries;

public interface IUserDictionaryRepository
{
    Task<bool> ExistsAsync(Guid userId, string Name, CancellationToken cancellation);
}
