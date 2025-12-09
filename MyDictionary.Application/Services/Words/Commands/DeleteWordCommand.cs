using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.DictionaryItems;

namespace MyDictionary.Application.Services.DictionaryItems.Commands;

public record DeleteWordCommand(Guid Id) : ICommand;

internal class DeleteWordCommandHandler(
    IAppDbContext appDbContext,
    SessionContext session
) : ICommandHandler<DeleteWordCommand>
{
    public async Task<Result> Handle(DeleteWordCommand command,
        CancellationToken cancellation)
    {
        var word = await appDbContext.Words
            .Where(item =>
                item.Dictionary.UserId == session.UserId &&
                item.Id == command.Id &&
                item.Deleted == null)
            .FirstOrDefaultAsync(cancellation);

        if (word == null) 
            return WordErrors.NotFound(command.Id);

        word.Deleted = DateTime.UtcNow;

        await appDbContext.SaveChangesAsync(cancellation);
        return Result.Success();
    }
}
