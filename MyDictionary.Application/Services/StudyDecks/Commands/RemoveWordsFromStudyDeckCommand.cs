using FluentValidation;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.StudyDecks;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;

namespace MyDictionary.Application.Services.StudyDecks.Commands;

public record RemoveWordsFromStudyDeckCommand(
    Guid StudyDeckId, 
    List<Guid> WordIds
) : ICommand;

public class RemoveWordsFromStudyDeckCommandValidator 
    : AbstractValidator<RemoveWordsFromStudyDeckCommand>
{
    public RemoveWordsFromStudyDeckCommandValidator()
    {
        RuleFor(x => x.StudyDeckId).NotEmpty();
        RuleFor(x => x.WordIds).NotEmpty();
    }
}

public class RemoveWordsFromStudyDeckCommandHandler(IAppDbContext context) 
    : ICommandHandler<RemoveWordsFromStudyDeckCommand>
{
    public async Task<Result> Handle(RemoveWordsFromStudyDeckCommand command, 
        CancellationToken cancellationToken)
    {
        var words = await context.StudyDeckWords
            .Where(x => 
                x.StudyDeckId == command.StudyDeckId && 
                command.WordIds.Contains(x.DictionaryItemId) &&
                x.Deleted == null)
            .ToListAsync(cancellationToken);

        if (words.Count == 0)
            return Result.Success();

        foreach (var word in words)
        {
            word.Deleted = DateTime.UtcNow;
        }

        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
