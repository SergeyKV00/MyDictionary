using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDictionary.Domain.Modules.StudyDecks;
using MyDictionary.Infrastructure.Persistence.Abstracts;

namespace MyDictionary.Infrastructure.Persistence.Configurations;

public class StudyDeckWordConfiguration : EntityConfiguration<StudyDeckWord>
{
    public override void Configure(EntityTypeBuilder<StudyDeckWord> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.StudyDeck)
            .WithMany(x => x.Words)
            .HasForeignKey(x => x.StudyDeckId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.DictionaryItem)
            .WithMany(x => x.StudyDeckWords)
            .HasForeignKey(x => x.DictionaryItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
