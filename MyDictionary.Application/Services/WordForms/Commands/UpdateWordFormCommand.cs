using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.DictionaryItems;
using MyDictionary.Domain.Modules.WordForms;

namespace MyDictionary.Application.Services.WordForms.Commands;

public record UpdateWordFormCommand(
    Guid Id,
    Optional<string?> Infinitive,
    Optional<string?> PastSimple,
    Optional<string?> PastParticiple
) : ICommand<Guid>;

internal class UpdateWordFormCommandHandler(
    IAppDbContext dbContext,
    SessionContext session
) : ICommandHandler<UpdateWordFormCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateWordFormCommand command,
        CancellationToken cancellation)
    {
        var wordForm = await dbContext.WordForms
            .FirstAsync(d =>
                d.Id == command.Id &&
                d.Deleted == null
            , cancellation);

        if (wordForm == null)
            return WordFormErrors.NotFound(command.Id);

        if (command.Infinitive.HasValue)
            wordForm.Infinitive = command.Infinitive.Value;
        if (command.PastSimple.HasValue)
            wordForm.PastSimple = command.PastSimple.Value;
        if (command.PastParticiple.HasValue)
            wordForm.PastParticiple = command.PastParticiple.Value;

        await dbContext.SaveChangesAsync(cancellation);
        return wordForm.Id;
    }
}