using FluentValidation;
using MyDictionary.Application.Interfaces;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.UserDictionaries;

namespace MyDictionary.Application.Services.UserDictionaries.Commands;

public record CreateUserDictionaryCommand(Guid UserId, string Name) : ICommand<Guid>;

internal class CreateUserDictionaryCommandHandler(
    IAppDbContext appDbContext,
    IUserDictionaryService service
)
        : ICommandHandler<CreateUserDictionaryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserDictionaryCommand command, 
        CancellationToken cancellation)
    {
        var isExists = await service.ExistsAsync(command.UserId, command.Name, cancellation);
        if (isExists)
            return UserDictionaryErrors.AlReadyExists(command.Name);

        var dictionary = new UserDictionary()
        {
            UserId = command.UserId,
            Name = command.Name
        };

        await appDbContext.UserDictionaries.AddAsync(dictionary, cancellation);
        await appDbContext.SaveChangesAsync(cancellation);

        return dictionary.Id;
    }
}

public class CreateUserDictionaryCommandValidator 
    : AbstractValidator<CreateUserDictionaryCommand>
{
    public CreateUserDictionaryCommandValidator()
    {
        RuleFor(command => command.UserId).NotEqual(Guid.Empty);
        RuleFor(command => command.Name).NotEmpty();
    }
}
