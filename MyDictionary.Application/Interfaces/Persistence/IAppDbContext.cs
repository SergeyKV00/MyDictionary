using Microsoft.EntityFrameworkCore;
using MyDictionary.Domain.Modules.DictionaryItemExamples;
using MyDictionary.Domain.Modules.DictionaryItems;
using MyDictionary.Domain.Modules.UserDictionaries;
using MyDictionary.Domain.Modules.Users;

namespace MyDictionary.Application.Interfaces.Persistence;

public interface IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserDictionary> UserDictionaries { get; set; }
    public DbSet<DictionaryItem> DictionaryItems { get; set; }
    public DbSet<DictionaryItemExample> DictionaryItemExamples { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToke);
}
