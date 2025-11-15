using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.UserDictionaries;

namespace MyDictionary.Application.Services.UserDictionaries.Commands;

public record DeleteUserDictionaryCommand(Guid Id) : ICommand;

internal class DeleteUserDictionaryCommandHandler(IAppDbContext appDbContext)
    : ICommandHandler<DeleteUserDictionaryCommand>
{
    public async Task<Result> Handle(DeleteUserDictionaryCommand command, 
        CancellationToken cancellation)
    {
        var dictionary = await appDbContext.UserDictionaries
            .Where(d => 
                d.Id == command.Id
                && d.Deleted == null)
            .FirstOrDefaultAsync(cancellation);

        if (dictionary == null)
            return UserDictionaryErrors.NotFound(command.Id);

        dictionary.Deleted = DateTime.UtcNow;
        await appDbContext.SaveChangesAsync(cancellation);

        return Result.Success();
    }
}
