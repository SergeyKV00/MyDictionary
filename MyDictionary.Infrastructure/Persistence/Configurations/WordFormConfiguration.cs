using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDictionary.Domain.Modules.WordForms;
using MyDictionary.Infrastructure.Persistence.Abstracts;

namespace MyDictionary.Infrastructure.Persistence.Configurations;

public class WordFormConfiguration : EntityConfiguration<WordForm>
{
    public override void Configure(EntityTypeBuilder<WordForm> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Infinitive)
            .HasMaxLength(250);

        builder.Property(x => x.PastSimple)
            .HasMaxLength(250);

        builder.Property(x => x.PastParticiple)
            .HasMaxLength(250);

        builder.HasOne(x => x.DictionaryItem)
            .WithOne(x => x.WordForm)
            .HasForeignKey<WordForm>(x => x.DictionaryItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
