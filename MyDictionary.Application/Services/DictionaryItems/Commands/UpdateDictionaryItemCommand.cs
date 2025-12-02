using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.DictionaryItems;

namespace MyDictionary.Application.Services.DictionaryItems.Commands;

public record UpdateDictionaryItemCommand(
    Guid Id,
    string? Term,
    string? Meaning,
    int? Weight
) : ICommand;

internal class UpdateDictionaryItemCommandHander(
    IAppDbContext appDbContext,
    SessionContext session
) : ICommandHandler<UpdateDictionaryItemCommand>
{
    public async Task<Result> Handle(UpdateDictionaryItemCommand command,
        CancellationToken cancellation)
    {
        var dictionaryItem = await appDbContext.DictionaryItems
            .Where(item => 
                item.Dictionary.UserId == session.UserId &&
                item.Id == command.Id
            )
            .FirstOrDefaultAsync(cancellation);

        if (dictionaryItem == null)
            return DictionaryItemErrors.NotFound(command.Id);

        if (command.Term != null) dictionaryItem.Term = command.Term;
        if (command.Meaning != null) dictionaryItem.Meaning = command.Meaning;
        if (command.Weight != null) dictionaryItem.Weight = command.Weight.Value;

        await appDbContext.SaveChangesAsync(cancellation);
        return Result.Success();
    }
}

public class UpdateDictionaryItemCommandValidator : AbstractValidator<UpdateDictionaryItemCommand>
{
    public UpdateDictionaryItemCommandValidator()
    {
        RuleFor(command => command.Id).NotEqual(Guid.Empty);

        RuleFor(command => command.Term)
            .MinimumLength(1)
            .When(x => x.Term != null);

        RuleFor(command => command.Meaning)
            .MinimumLength(1)
            .When(x => x.Meaning != null);
    }
}
