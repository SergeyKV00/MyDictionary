using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.Users;

namespace MyDictionary.Application.Services.Users.Queries;

public record GetUserQuery(Guid Id) : IQuery<User>;

internal class GetUserQueryHandler(IAppDbContext appDbContext)
    : IQueryHandler<GetUserQuery, User>
{
    public async Task<Result<User>> Handle(GetUserQuery query, CancellationToken cancellation)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(user =>
            user.Id == query.Id
        );

        var error = UserErrors.NotFound(query.Id);

        if (user == null)
         return UserErrors.NotFound(query.Id);

        return user;
    }
}

