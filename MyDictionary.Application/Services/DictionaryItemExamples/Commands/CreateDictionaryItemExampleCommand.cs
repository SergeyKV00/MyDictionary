using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.DictionaryItemExamples;
using MyDictionary.Domain.Modules.DictionaryItems;

namespace MyDictionary.Application.Services.DictionaryItemExamples.Commands;

public record class CreateDictionaryItemExampleCommand(
    Guid DictionaryItemId,
    string Example,
    string? Translation
) : ICommand<Guid>;

internal class CreateDictionaryItemExampleCommandHandler(
    IAppDbContext dbContext,
    SessionContext session
) : ICommandHandler<CreateDictionaryItemExampleCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateDictionaryItemExampleCommand command,
        CancellationToken cancellation)
    {
        var dictionaryItem = await dbContext.DictionaryItems
            .FirstOrDefaultAsync(d =>
                d.Dictionary.UserId == session.UserId &&
                d.Id == command.DictionaryItemId &&
                d.Deleted == null,
            cancellation);

        if (dictionaryItem == null)
            return DictionaryItemErrors.NotFound(command.DictionaryItemId);

        var exampleNew = new DictionaryItemExample
        {
            DictionaryItemId = command.DictionaryItemId,
            Example = command.Example,
            Translation = command.Translation
        };

        dbContext.DictionaryItemExamples.Add(exampleNew);
        await dbContext.SaveChangesAsync(cancellation);

        return exampleNew.Id;
    }
}

public class CreateDictionaryItemExampleValidator 
    : AbstractValidator<CreateDictionaryItemExampleCommand>
{
    public CreateDictionaryItemExampleValidator()
    {
        RuleFor(command => command.DictionaryItemId).NotEqual(Guid.Empty);
        RuleFor(command => command.Example).NotEmpty();
    }
}
