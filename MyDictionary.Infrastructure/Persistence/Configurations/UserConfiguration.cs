using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDictionary.Domain.Users;
using MyDictionary.Infrastructure.Persistence.Abstracts;

namespace MyDictionary.Infrastructure.Persistence.Configurations;

public class UserConfiguration : EntityConfiguration<User>
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
