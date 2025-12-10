using FSRS.Constants;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.DictionaryItems;
using MyDictionary.Domain.Modules.WordProgresses;

namespace MyDictionary.Application.Services.WordProgresses.Commands;

public record CreateWordProgressCommand(Guid DictionaryItemId)
    :ICommand<Guid>;

internal class CreateWordProgressCommandHandler(
    IAppDbContext dbContext,
    SessionContext session
) : ICommandHandler<CreateWordProgressCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateWordProgressCommand command,
        CancellationToken cancellation)
    {
        var dictionaryItem = await dbContext.DictionaryItems
            .FirstOrDefaultAsync(d =>
                d.Id == command.DictionaryItemId &&
                d.Dictionary.UserId == session.UserId &&
                d.Deleted == null
            , cancellation);

        if (dictionaryItem == null)
            return DictionaryItemErrors.NotFound(command.DictionaryItemId);

        var exists = await dbContext.WordProgresses
            .FirstOrDefaultAsync(d => 
                d.DictionaryItemId == command.DictionaryItemId &&
                d.UserId == session.UserId &&
                d.Deleted == null
            , cancellation);

        if (exists != null)
            return exists.Id;

        var wordProgress = new WordProgress
        {
            UserId = session.UserId,
            DictionaryItemId = command.DictionaryItemId,
            NextReview = DateTime.UtcNow,
            State = CardState.New
        };

        dbContext.WordProgresses.Add(wordProgress);
        await dbContext.SaveChangesAsync(cancellation);
        return wordProgress.Id;
    }
}
