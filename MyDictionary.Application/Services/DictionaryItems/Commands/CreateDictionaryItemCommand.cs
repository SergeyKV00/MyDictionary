using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.DictionaryItems;
using MyDictionary.Domain.Modules.UserDictionaries;

namespace MyDictionary.Application.Services.DictionaryItems.Commands;

public record CreateDictionaryItemCommand (
    Guid DictionaryId,
    string Term,
    string Meaning,
    int Weight
): ICommand<Guid>;

internal class CreateDictionaryItemCommandHandler(
    IAppDbContext appDbContext, 
    SessionContext session
) : ICommandHandler<CreateDictionaryItemCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateDictionaryItemCommand command, 
        CancellationToken cancellation)
    {
        var dictionary = await appDbContext.UserDictionaries
            .FirstOrDefaultAsync(d =>
                d.Id == command.DictionaryId &&
                d.UserId == session.UserId &&
                d.Deleted == null, 
            cancellationToken: cancellation);

        if (dictionary == null)
            return UserDictionaryErrors.NotFound(command.DictionaryId);

        var item = new DictionaryItem
        {
            DictionaryId = command.DictionaryId,
            Term = command.Term,
            Meaning = command.Meaning,
            Weight = command.Weight
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
