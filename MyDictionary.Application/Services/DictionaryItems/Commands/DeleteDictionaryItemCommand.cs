using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.DictionaryItems;

namespace MyDictionary.Application.Services.DictionaryItems.Commands;

public record DeleteDictionaryItemCommand(Guid Id) : ICommand;

internal class DeleteDictionaryItemCommandHandler(IAppDbContext appDbContext)
    : ICommandHandler<DeleteDictionaryItemCommand>
{
    public async Task<Result> Handle(DeleteDictionaryItemCommand command,
        CancellationToken cancellation)
    {
        var dictionaryItem = await appDbContext.DictionaryItems
            .Where(item =>
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
