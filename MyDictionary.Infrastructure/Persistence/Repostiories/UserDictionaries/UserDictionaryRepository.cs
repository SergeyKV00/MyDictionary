using Microsoft.EntityFrameworkCore;
using MyDictionary.Domain.Modules.UserDictionaries;

namespace MyDictionary.Infrastructure.Persistence.Repostiories.UserDictionaries;

public class UserDictionaryRepository : IUserDictionaryRepository
{
    private readonly AppDbContext _context;
    public UserDictionaryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Guid userId, string name, CancellationToken cancellation)
    {
        return await _context.UserDictionaries
            .AnyAsync(d => 
                d.UserId == userId 
                && d.Name == name
                && d.Deleted == null
            , cancellation);
    }
}
