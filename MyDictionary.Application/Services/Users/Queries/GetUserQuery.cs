using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Common.Mappings;
using MyDictionary.Application.Interfaces;
using MyDictionary.Application.Services.Users.Models;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Users;

namespace MyDictionary.Application.Services.Users.Queries;

public record GetUserQuery(Guid Id) : IQuery<UserVm>;

internal class GetUserQueryHandler(IAppDbContext appDbContext)
    : IQueryHandler<GetUserQuery, UserVm>
{
    public async Task<Result<UserVm>> Handle(GetUserQuery query, CancellationToken cancellation)
    {
        var user = await appDbContext.Users.FirstOrDefaultAsync(user =>
            user.Id == query.Id
        );

        var error = UserErrors.NotFound(query.Id);

        if (user == null)
         return UserErrors.NotFound(query.Id);

        return user.ToView();
    }
}

