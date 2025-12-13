using FluentValidation;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.StudyDecks;

namespace MyDictionary.Application.Services.StudyDecks.Commands;

public record AddWordsToStudyDeckCommand(
    Guid StudyDeckId, 
    List<Guid> WordIds
) : ICommand;

public class AddWordsToStudyDeckCommandHandler(IAppDbContext context) 
    : ICommandHandler<AddWordsToStudyDeckCommand>
{
    public async Task<Result> Handle(AddWordsToStudyDeckCommand command, 
        CancellationToken cancellationToken)
    {
         var studyDeck = await context.StudyDecks.FindAsync([command.StudyDeckId], cancellationToken);
         if (studyDeck == null)
             return StudyDeckErrors.NotFound(command.StudyDeckId);

         var existingWordIds = context.StudyDeckWords
             .Where(x => 
                 x.StudyDeckId == command.StudyDeckId && 
                 command.WordIds.Contains(x.DictionaryItemId) && 
                 x.Deleted == null
             )
             .Select(x => x.DictionaryItemId)
             .ToHashSet();

         var newWords = command.WordIds
             .Where(id => !existingWordIds.Contains(id))
             .Select(id => new StudyDeckWord
             {
                 StudyDeckId = command.StudyDeckId,
                 DictionaryItemId = id,
                 Created = DateTime.UtcNow
             });

         context.StudyDeckWords.AddRange(newWords);
         await context.SaveChangesAsync(cancellationToken);

         return Result.Success();
    }
}
