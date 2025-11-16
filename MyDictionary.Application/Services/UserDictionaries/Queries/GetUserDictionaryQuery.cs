using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.UserDictionaries;

namespace MyDictionary.Application.Services.UserDictionaries.Queries;

public record GetUserDictionaryQuery(Guid Id) : IQuery<UserDictionary>;

internal class GetUserDictionaryQueryHandler(IAppDbContext dbContext)
    : IQueryHandler<GetUserDictionaryQuery, UserDictionary>
{
    public async Task<Result<UserDictionary>> Handle(GetUserDictionaryQuery query, 
        CancellationToken cancellation)
    {
        var dictionary = await dbContext.UserDictionaries
            .FirstOrDefaultAsync(d =>
                d.Deleted == null &&
                d.Id == query.Id);

        if (dictionary == null)
            return UserDictionaryErrors.NotFound(query.Id);

        return Result.Success(dictionary);
    }
}
