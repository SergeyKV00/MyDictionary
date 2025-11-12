using MediatR;
using MyDictionary.Application.Interfaces;
using MyDictionary.Domain.Users;

namespace MyDictionary.Application.Services.Users.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IAppDbContext appDbContext;
    public CreateUserCommandHandler(IAppDbContext appDbContext)
        => this.appDbContext = appDbContext;

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email,
            Password = request.Password,
        };

        await appDbContext.Users.AddAsync(user, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
