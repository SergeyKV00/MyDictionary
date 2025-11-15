using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.UserDictionaries;

namespace MyDictionary.Application.Services.UserDictionaries.Commands;

public record UpdateUserDictionaryCommand(Guid Id, Guid UserId, string Name) : ICommand;

internal class UpdateUserDictionaryCommandHandler(
    IAppDbContext appDbContext, 
    IUserDictionaryService service
) 
    : ICommandHandler<UpdateUserDictionaryCommand>
{
    public async Task<Result> Handle(UpdateUserDictionaryCommand command,
        CancellationToken cancellation)
    {
        var validationResult = await ServerValidation(command, cancellation);
        if (validationResult.IsFailure)
            return validationResult;

        var dictionary = validationResult.Value;

        dictionary.Name = command.Name;
        await appDbContext.SaveChangesAsync(cancellation);

        return Result.Success();
    }

    private async Task<Result<UserDictionary>> ServerValidation(UpdateUserDictionaryCommand command, 
        CancellationToken cancellation)
    {
        var dictionary = await appDbContext.UserDictionaries
            .Where(d => d.Id == command.Id)
            .FirstOrDefaultAsync(cancellation);

        if (dictionary == null)
            return UserDictionaryErrors.NotFound(command.Id);

        var isExists = await service.ExistsAsync(command.UserId, command.Name, cancellation);
        if (isExists)
            return UserDictionaryErrors.AlReadyExists(command.Name);

        return dictionary;
    }
}

public class UpdateUserDictionaryCommandValidator : AbstractValidator<UpdateUserDictionaryCommand>
{
    public UpdateUserDictionaryCommandValidator()
    {
        RuleFor(command => command.Id).NotEqual(Guid.Empty);
        RuleFor(command => command.Name).NotEmpty();
    }
}
