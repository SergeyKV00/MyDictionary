using Microsoft.EntityFrameworkCore;
using MyDictionary.Domain.Entities;

namespace MyDictionary.Application.Interfaces;

public interface IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserDictionary> UserDictionaries { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToke);
}
