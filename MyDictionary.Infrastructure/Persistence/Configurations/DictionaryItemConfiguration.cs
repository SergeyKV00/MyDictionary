using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDictionary.Domain.UserDictionaryItems;
using MyDictionary.Infrastructure.Persistence.Abstracts;

namespace MyDictionary.Infrastructure.Persistence.Configurations;

public class DictionaryItemConfiguration : EntityConfiguration<DictionaryItem>
{
    public override void Configure(EntityTypeBuilder<DictionaryItem> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Term)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Meaning)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasOne(x => x.Dictionary)
            .WithMany(d => d.Items)
            .HasForeignKey(x => x.DictionaryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
