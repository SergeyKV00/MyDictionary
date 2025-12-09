using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.DictionaryItems;
using MyDictionary.Domain.Modules.WordForms;

namespace MyDictionary.Application.Services.WordForms.Commands;

public record CreateWordFormCommand(
    Guid DictionaryItemId,
    string? Infinitive,
    string? PastSimple,
    string? PastParticiple
) : ICommand<Guid>;

internal class CreateWordFormCommandHandler(
    IAppDbContext dbContext,
    SessionContext session
) : ICommandHandler<CreateWordFormCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateWordFormCommand command,
        CancellationToken cancellation)
    {
        var dictionaryItem = await dbContext.Words
            .FirstAsync(d =>
                d.Id == command.DictionaryItemId &&
                d.Dictionary.UserId == session.UserId &&
                d.Deleted == null
            , cancellation);

        if (dictionaryItem == null)
            return WordErrors.NotFound(command.DictionaryItemId);

        var wordForm = new WordForm()
        {
            DictionaryItemId = command.DictionaryItemId,
            Infinitive = command.Infinitive,
            PastSimple = command.PastSimple,
            PastParticiple = command.PastParticiple
        };

        dbContext.WordForms.Add(wordForm);
        await dbContext.SaveChangesAsync(cancellation);
        return wordForm.Id;
    }
}