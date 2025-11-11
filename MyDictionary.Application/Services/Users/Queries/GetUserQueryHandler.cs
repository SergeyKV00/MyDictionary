using MediatR;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Common.Exceptions;
using MyDictionary.Application.Common.Mappings;
using MyDictionary.Application.Interfaces;
using MyDictionary.Application.Services.Users.Models;

namespace MyDictionary.Application.Services.Users.Queries;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserVm>
{
    private readonly IAppDbContext appDbContext;
    public GetUserQueryHandler(IAppDbContext appDbContext)
        => this.appDbContext = appDbContext;

    public async Task<UserVm> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(user =>
            user.Id == request.Id
        );

        if (user == null)
            throw new NotFoundExceptions(nameof(user), request.Id);

        return user.ToView();
    }
}
