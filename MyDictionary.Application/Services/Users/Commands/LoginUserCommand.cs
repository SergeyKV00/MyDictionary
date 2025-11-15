using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Application.Interfaces.Services;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.Users;

namespace MyDictionary.Application.Services.Users.Commands;

public record LoginUserCommand(string UserName, string Password) : ICommand<string>;

internal class LoginUserCommandHandler(
    IAppDbContext dbContext, 
    IPasswordService passwordService,
    ITokenProvider tokenProvider)
    : ICommandHandler<LoginUserCommand, string>
{
    public async Task<Result<string>> Handle(LoginUserCommand command, 
        CancellationToken cancellation)
    {
        var user = await dbContext.Users
            .Where(u =>
                u.Deleted == null &&
                u.UserName == command.UserName)
            .FirstOrDefaultAsync(cancellation);

        if (user == null)
            return UserErrors.InvalidCredentials();

        var verified = passwordService.Verify(command.Password, user.Password);
        if (!verified)
            return UserErrors.InvalidCredentials();

        var token = tokenProvider.Create(user);
        return token;
    }
}

public class LoginUserCommandValiadator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValiadator()
    {
        RuleFor(command => command.UserName).NotEmpty();
        RuleFor(command => command.Password).NotEmpty();
    }
}

