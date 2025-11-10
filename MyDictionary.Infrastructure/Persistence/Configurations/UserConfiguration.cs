using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDictionary.Domain.Entities;

namespace MyDictionary.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : EntityBaseConfiguration<User, Guid>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(entity => entity.UserName).HasMaxLength(250);
        }
    }
}
