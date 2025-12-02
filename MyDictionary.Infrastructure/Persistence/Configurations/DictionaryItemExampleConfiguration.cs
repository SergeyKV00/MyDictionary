using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Domain.Modules.DictionaryItemExamples;
using MyDictionary.Infrastructure.Persistence.Abstracts;

namespace MyDictionary.Infrastructure.Persistence.Configurations;

public class DictionaryItemExampleConfiguration : EntityConfiguration<DictionaryItemExample>
{
    public override void Configure(EntityTypeBuilder<DictionaryItemExample> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Example)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.Translation)
            .HasMaxLength(1000);

        builder.HasOne(x => x.DictionaryItem)
            .WithMany(d => d.Examples)
            .HasForeignKey(x => x.DictionaryItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
