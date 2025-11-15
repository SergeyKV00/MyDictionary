namespace MyDictionary.Domain.Modules.UserDictionaries;

public class UserDictionaryService : IUserDictionaryService
{
    private readonly IUserDictionaryRepository _userDictionaryService;

    public UserDictionaryService(IUserDictionaryRepository userDictionaryService)
        => _userDictionaryService = userDictionaryService;

    public async Task<bool> ExistsAsync(Guid userId, string name, CancellationToken cancellation)
    {
        return await _userDictionaryService.ExistsAsync(userId, name, cancellation);
    }
}
