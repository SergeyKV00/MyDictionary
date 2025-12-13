using FluentValidation;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.StudyDecks;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;

namespace MyDictionary.Application.Services.StudyDecks.Commands;

public record RemoveDictionaryFromStudyDeckCommand(
    Guid StudyDeckId, 
    Guid DictionaryId
) : ICommand;

public class RemoveDictionaryFromStudyDeckCommandValidator 
    : AbstractValidator<RemoveDictionaryFromStudyDeckCommand>
{
    public RemoveDictionaryFromStudyDeckCommandValidator()
    {
        RuleFor(x => x.StudyDeckId).NotEmpty();
        RuleFor(x => x.DictionaryId).NotEmpty();
    }
}

public class RemoveDictionaryFromStudyDeckCommandHandler(IAppDbContext context) 
    : ICommandHandler<RemoveDictionaryFromStudyDeckCommand>
{
    public async Task<Result> Handle(RemoveDictionaryFromStudyDeckCommand command, 
        CancellationToken cancellationToken)
    {
        var link = await context.StudyDeckDictionaries
            .FirstOrDefaultAsync(x => 
                x.StudyDeckId == command.StudyDeckId && 
                x.DictionaryId == command.DictionaryId &&
                x.Deleted == null, 
            cancellationToken);

        if (link == null)
            return StudyDeckErrors.DictionaryNotFound(command.DictionaryId);

        link.Deleted = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
