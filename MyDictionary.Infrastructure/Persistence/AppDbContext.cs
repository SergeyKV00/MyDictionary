using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Interfaces;
using MyDictionary.Domain.Modules.DictionaryItemExamples;
using MyDictionary.Domain.Modules.DictionaryItems;
using MyDictionary.Domain.Modules.UserDictionaries;
using MyDictionary.Domain.Modules.Users;
using MyDictionary.Domain.Modules.WordForms;
using System.Reflection;

namespace MyDictionary.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserDictionary> UserDictionaries { get; set; }
    public DbSet<Word> Words { get; set; }
    public DbSet<DictionaryItemExample> DictionaryItemExamples { get; set; }
    public DbSet<WordForm> WordForms { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.DetectChanges();
        UpdateAuditable();

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    private void UpdateAuditable()
    {
        foreach (var tracker in ChangeTracker.Entries<IAuditable>())
        {
            switch (tracker.State)
            {
                case EntityState.Added:
                    tracker.Entity.Created = DateTime.UtcNow;
                    break;
            }
        }
    }
}
