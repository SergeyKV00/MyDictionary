using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDictionary.Domain.Modules.StudyDecks;
using MyDictionary.Infrastructure.Persistence.Abstracts;

namespace MyDictionary.Infrastructure.Persistence.Configurations;

public class StudyDeckDictionaryConfiguration : EntityConfiguration<StudyDeckDictionary>
{
    public override void Configure(EntityTypeBuilder<StudyDeckDictionary> builder)
    {
        base.Configure(builder);

        builder.HasOne(x => x.StudyDeck)
            .WithMany(x => x.Dictionaries)
            .HasForeignKey(x => x.StudyDeckId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Dictionary)
            .WithMany(x => x.StudyDeckDictionaries)
            .HasForeignKey(x => x.DictionaryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
