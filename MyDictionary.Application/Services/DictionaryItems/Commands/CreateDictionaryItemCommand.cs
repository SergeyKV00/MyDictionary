using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.UserDictionaries;
using MyDictionary.Domain.UserDictionaryItems;

namespace MyDictionary.Application.Services.DictionaryItems.Commands;

public record CreateDictionaryItemCommand (
    Guid DictionaryId,
    string Term,
    string Meaning
): ICommand<Guid>;

internal class CreateDictionaryItemCommandHandler(IAppDbContext appDbContext)
    : ICommandHandler<CreateDictionaryItemCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateDictionaryItemCommand command, CancellationToken cancellation)
    {
        //TODO Check owner dictionary is current user
        var dictionary = await appDbContext.UserDictionaries
            .FirstOrDefaultAsync(d =>
                d.Id == command.DictionaryId &&
                d.Deleted == null, 
            cancellationToken: cancellation);

        if (dictionary == null)
            return UserDictionaryErrors.NotFound(command.DictionaryId);

        // TODO Exists Validation

        var item = new DictionaryItem
        {
            DictionaryId = command.DictionaryId,
            Term = command.Term,
            Meaning = command.Meaning,
            Weight = 0
        };
        appDbContext.DictionaryItems.Add(item);
        await appDbContext.SaveChangesAsync(cancellation);

        return item.Id;
    }
}

public class CreateDictionaryItemCommandValidator : AbstractValidator<CreateDictionaryItemCommand>
{
    public CreateDictionaryItemCommandValidator()
    {
        RuleFor(command => command.DictionaryId).NotEqual(Guid.Empty);
        RuleFor(command => command.Term).NotEmpty();
        RuleFor(command => command.Meaning).NotEmpty();
    }
}
