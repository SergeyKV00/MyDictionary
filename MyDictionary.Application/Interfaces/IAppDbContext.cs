using Microsoft.EntityFrameworkCore;
using MyDictionary.Domain.UserDictionaries;
using MyDictionary.Domain.Users;

namespace MyDictionary.Application.Interfaces;

public interface IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserDictionary> UserDictionaries { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToke);
}
