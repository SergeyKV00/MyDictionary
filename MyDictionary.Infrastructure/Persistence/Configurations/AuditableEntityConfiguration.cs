using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDictionary.Domain.Interfaces;

namespace MyDictionary.Infrastructure.Persistence.Configurations
{
    public static class AuditableEntityConfiguration
    {
        public static void Configure(EntityTypeBuilder builder)
        {
            builder.Property("Created")
                .HasColumnType("TEXT")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property("Deleted")
                .HasColumnType("TEXT");
        }
    }
}
