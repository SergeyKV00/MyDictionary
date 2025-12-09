using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.DictionaryItems;

namespace MyDictionary.Application.Services.DictionaryItems.Commands;

public record UpdateWordCommand(
    Guid Id,
    string? Term,
    string? Meaning,
    int? Weight
) : ICommand<Guid>;

internal class UpdateWordCommandHander(
    IAppDbContext appDbContext,
    SessionContext session
) : ICommandHandler<UpdateWordCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateWordCommand command,
        CancellationToken cancellation)
    {
        var word = await appDbContext.Words
            .Where(item => 
                item.Dictionary.UserId == session.UserId &&
                item.Id == command.Id
            )
            .FirstOrDefaultAsync(cancellation);

        if (word == null)
            return WordErrors.NotFound(command.Id);

        if (command.Term != null) word.Term = command.Term;
        if (command.Meaning != null) word.Meaning = command.Meaning;
        if (command.Weight != null) word.Weight = command.Weight.Value;

        await appDbContext.SaveChangesAsync(cancellation);
        return word.Id;
    }
}

public class UpdateWordCommandValidator : AbstractValidator<UpdateWordCommand>
{
    public UpdateWordCommandValidator()
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
