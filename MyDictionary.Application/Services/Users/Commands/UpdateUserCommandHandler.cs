using MediatR;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Persistence;

namespace MyDictionary.Application.Services.Users.Commands;

// TODO
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IAppDbContext appDbContext;
    public UpdateUserCommandHandler(IAppDbContext appDbContext)
        => this.appDbContext = appDbContext;

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(user =>
            user.Id == request.Id
        );

        if (user == null)
            throw new Exception(nameof(user));

        user.UserName = request.UserName;
        user.Email = request.Email;
        user.Password = request.Password;

        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
