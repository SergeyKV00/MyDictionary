using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.DictionaryItemExamples;

namespace MyDictionary.Application.Services.DictionaryItemExamples.Commands;

public record DeleteDictionaryItemExampleCommand(Guid Id) : ICommand
{
    public class Handler(
        IAppDbContext dbContext,
        SessionContext session
    ) : ICommandHandler<DeleteDictionaryItemExampleCommand>
    {
        public async Task<Result> Handle(DeleteDictionaryItemExampleCommand command, 
            CancellationToken cancellation)
        {
            var example = await dbContext.DictionaryItemExamples
                .FirstOrDefaultAsync(d =>
                    d.DictionaryItem.Dictionary.UserId == session.UserId &&
                    d.Deleted == null &&
                    d.Id == command.Id,
                cancellation);

            if (example == null)
                return DictionaryItemExampleErrors.NotFound(command.Id);

            example.Deleted = DateTime.UtcNow;
            
            await dbContext.SaveChangesAsync(cancellation);
            return Result.Success();
        }
    }
}
