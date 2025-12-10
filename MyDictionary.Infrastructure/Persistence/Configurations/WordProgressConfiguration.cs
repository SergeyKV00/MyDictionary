using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDictionary.Domain.Modules.WordProgresses;
using MyDictionary.Infrastructure.Persistence.Abstracts;

namespace MyDictionary.Infrastructure.Persistence.Configurations;

public class WordProgressConfiguration : EntityConfiguration<WordProgress>
{
    public override void Configure(EntityTypeBuilder<WordProgress> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.DictionaryItem)
            .WithOne(x => x.WordProgress)
            .HasForeignKey<WordProgress>(x => x.DictionaryItemId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.User)
            .WithMany(x => x.WordProgresses)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
