using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces;
using MyDictionary.Domain.Entities;
using System.Reflection;

namespace MyDictionary.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserDictionary> UserDictionaries { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyAuditableConfiguration();

            base.OnModelCreating(modelBuilder);
        }
    }
}
