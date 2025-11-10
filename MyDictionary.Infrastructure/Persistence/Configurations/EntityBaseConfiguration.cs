using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Domain.Interfaces;

namespace MyDictionary.Infrastructure.Persistence.Configurations
{
    public class EntityBaseConfiguration<TEntity, TIdentifier> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IEntity<TIdentifier>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(entity => entity.Id).IsUnique();

            if (typeof(TIdentifier) == typeof(Guid))
                builder.Property(e => e.Id).HasColumnType("uniqueidentifier");
            else if (typeof(TIdentifier) == typeof(int))
                builder.Property(e => e.Id).HasColumnType("INTEGER");
        }
    }
}
