using FluentValidation;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.StudyDecks;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;

namespace MyDictionary.Application.Services.StudyDecks.Commands;

public record AddDictionaryToStudyDeckCommand(
    Guid StudyDeckId, 
    Guid DictionaryId, 
    bool IsSynchronized
) : ICommand;

public class AddDictionaryToStudyDeckCommandValidator 
    : AbstractValidator<AddDictionaryToStudyDeckCommand>
{
    public AddDictionaryToStudyDeckCommandValidator()
    {
        RuleFor(x => x.StudyDeckId).NotEmpty();
        RuleFor(x => x.DictionaryId).NotEmpty();
    }
}

public class AddDictionaryToStudyDeckCommandHandler(IAppDbContext context) 
    : ICommandHandler<AddDictionaryToStudyDeckCommand>
{
    public async Task<Result> Handle(AddDictionaryToStudyDeckCommand command, 
        CancellationToken cancellationToken)
    {
        var studyDeck = await context.StudyDecks.FindAsync([command.StudyDeckId], cancellationToken);

        if (studyDeck == null)
            return StudyDeckErrors.NotFound(command.StudyDeckId);

        var existingLink = await context.StudyDeckDictionaries
            .FirstOrDefaultAsync(x => 
                x.StudyDeckId == command.StudyDeckId && 
                x.DictionaryId == command.DictionaryId &&
                x.Deleted == null,
            cancellationToken);

        if (existingLink != null)
            return StudyDeckErrors.DictionaryAlreadyExists(command.DictionaryId);

        var link = new StudyDeckDictionary
        {
            StudyDeckId = command.StudyDeckId,
            DictionaryId = command.DictionaryId,
            IsSynchronized = command.IsSynchronized,
            Created = DateTime.UtcNow
        };
        context.StudyDeckDictionaries.Add(link);

        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
