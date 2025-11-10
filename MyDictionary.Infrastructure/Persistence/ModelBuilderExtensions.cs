using Microsoft.EntityFrameworkCore;
using MyDictionary.Domain.Interfaces;
using MyDictionary.Infrastructure.Persistence.Configurations;

namespace MyDictionary.Infrastructure.Persistence
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyAuditableConfiguration(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IAuditable).IsAssignableFrom(entityType.ClrType))
                {
                    AuditableEntityConfiguration.Configure(modelBuilder.Entity(entityType.ClrType));
                }
            }
        }
    }
}
