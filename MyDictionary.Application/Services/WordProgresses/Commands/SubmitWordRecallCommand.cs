using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.WordProgresses;

namespace MyDictionary.Application.Services.WordProgresses.Commands;

public record SubmitWordRecallCommand(Guid DictionaryItemId, ProgressQuality Rating): ICommand;

internal class SubmitWordRecallCommandHandler(
    IAppDbContext dbContext,
    SessionContext session,
    IWordProgressService wordProgressService
) : ICommandHandler<SubmitWordRecallCommand>
{
    public async Task<Result> Handle(SubmitWordRecallCommand command,
        CancellationToken cancellation)
    {
        var wordProgress = await dbContext.WordProgresses
            .FirstOrDefaultAsync(d =>
                d.DictionaryItemId == command.DictionaryItemId &&
                d.UserId == session.UserId &&
                d.Deleted == null
            , cancellation);

        if (wordProgress == null)
            return WordProgressErrors.NotFound();

        wordProgressService.ApplyReview(wordProgress, command.Rating);

        await dbContext.SaveChangesAsync(cancellation);
        return Result.Success();
    }
}
