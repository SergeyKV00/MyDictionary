using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDictionary.Domain.UserDictionaries;
using MyDictionary.Infrastructure.Persistence.Abstracts;

namespace MyDictionary.Infrastructure.Persistence.Configurations;

public class UserDictionaryConfiguration : EntityConfiguration<UserDictionary>
{
    public override void Configure(EntityTypeBuilder<UserDictionary> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(u => u.UserDictionaries)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
