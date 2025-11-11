using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces;
using MyDictionary.Domain.Entities;
using MyDictionary.Domain.Interfaces;
using System.Reflection;

namespace MyDictionary.Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserDictionary> UserDictionaries { get; set; }

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
                case EntityState.Modified:
                    tracker.Entity.Created = DateTime.UtcNow;
                    break;
            }
        }
    }
}
