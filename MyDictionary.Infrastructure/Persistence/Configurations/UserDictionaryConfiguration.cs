using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDictionary.Domain.Entities;

namespace MyDictionary.Infrastructure.Persistence.Configurations
{
    public class UserDictionaryConfiguration : EntityBaseConfiguration<UserDictionary, Guid>
    {
        public override void Configure(EntityTypeBuilder<UserDictionary> builder)
        {
            base.Configure(builder);
            builder.Property(entity => entity.UserId).HasColumnType("uniqueidentifier");
        }
    }
}
