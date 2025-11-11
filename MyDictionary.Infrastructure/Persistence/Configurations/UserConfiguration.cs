using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDictionary.Domain.Entities;

namespace MyDictionary.Infrastructure.Persistence.Configurations;

public class UserConfiguration : EntityBaseConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.UserName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.Password)
            .HasMaxLength(300)
            .IsRequired();
    }
}
