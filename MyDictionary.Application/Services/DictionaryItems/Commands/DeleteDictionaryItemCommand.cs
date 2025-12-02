using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.DictionaryItems;

namespace MyDictionary.Application.Services.DictionaryItems.Commands;

public record DeleteDictionaryItemCommand(Guid Id) : ICommand;

internal class DeleteDictionaryItemCommandHandler(
    IAppDbContext appDbContext,
    SessionContext session
) : ICommandHandler<DeleteDictionaryItemCommand>
{
    public async Task<Result> Handle(DeleteDictionaryItemCommand command,
        CancellationToken cancellation)
    {
        var dictionaryItem = await appDbContext.DictionaryItems
            .Where(item =>
                item.Dictionary.UserId == session.UserId &&
                item.Id == command.Id &&
                item.Deleted == null)
            .FirstOrDefaultAsync(cancellation);

        if (dictionaryItem == null) 
            return DictionaryItemErrors.NotFound(command.Id);

        dictionaryItem.Deleted = DateTime.UtcNow;

        await appDbContext.SaveChangesAsync(cancellation);
        return Result.Success();
    }
}
