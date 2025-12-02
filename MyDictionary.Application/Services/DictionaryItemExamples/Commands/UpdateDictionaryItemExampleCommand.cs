using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.DictionaryItemExamples;

namespace MyDictionary.Application.Services.DictionaryItemExamples.Commands;

public record UpdateDictionaryItemExampleCommand(
    Guid Id,
    string Example,
    Optional<string?> Translation
) : ICommand
{
    public class Handler(
        IAppDbContext dbContext,
        SessionContext session
    ) : ICommandHandler<UpdateDictionaryItemExampleCommand>
    {
        public async Task<Result> Handle(UpdateDictionaryItemExampleCommand command, 
            CancellationToken cancellation)
        {
            var example = await dbContext.DictionaryItemExamples
                .FirstOrDefaultAsync(d =>
                    d.DictionaryItem.Dictionary.UserId == session.UserId &&
                    d.Id == command.Id,
                cancellation);

            if (example == null)
                return DictionaryItemExampleErrors.NotFound(command.Id);

            example.Example = command.Example;
            if (command.Translation.HasValue)
                example.Translation = command.Translation.Value;

            await dbContext.SaveChangesAsync(cancellation);
            return Result.Success();
        }
    }
}
