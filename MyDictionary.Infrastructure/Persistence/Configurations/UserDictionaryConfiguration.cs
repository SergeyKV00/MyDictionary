using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDictionary.Domain.Modules.UserDictionaries;
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
            .WithMany(u => u.Dictionaries)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
