using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Application.Interfaces.Services;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.Users;

namespace MyDictionary.Application.Services.Users.Commands;

public record CreateUserCommand(
    string UserName,
    string Email,
    string Password) 
: ICommand;

internal class CreateUserCommandHander(IAppDbContext dbContext, IPasswordService passwordService)
    : ICommandHandler<CreateUserCommand>
{
    public async Task<Result> Handle(CreateUserCommand command, CancellationToken cancellation)
    {
        var user = await dbContext.Users
            .Where(u => u.Deleted == null)
            .Where(u => 
                u.UserName == command.UserName ||
                u.Email == command.Email)
            .FirstOrDefaultAsync(cancellation);

        if (user != null)
        {
            if (user.UserName == command.UserName)
                return UserErrors.UserNameAlreadyTaken(command.UserName);
            if (user.Email == command.Email)
                return UserErrors.EmailAlreadyUse(command.Email);
        }

        var newUser = new User
        {
            UserName = command.UserName,
            Email = command.Email,
            Password = passwordService.GenerateHash(command.Password)
        };

        await dbContext.Users.AddAsync(newUser, cancellation);
        await dbContext.SaveChangesAsync(cancellation);
        return Result.Success();
    }
}

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.UserName).NotEmpty();
        RuleFor(command => command.Email).NotEmpty();
        RuleFor(command => command.Password).NotEmpty();
    }
}
