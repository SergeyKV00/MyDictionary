using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Common;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.UserDictionaries;

namespace MyDictionary.Application.Services.UserDictionaries.Queries;

public record GetUserDictionaryListQuery(Guid UserId) : IQuery<ListModel<UserDictionary>>;

internal class GetUserDictionaryListQueryHandler(IAppDbContext dbContext)
    : IQueryHandler<GetUserDictionaryListQuery, ListModel<UserDictionary>>

{
    public async Task<Result<ListModel<UserDictionary>>> Handle(GetUserDictionaryListQuery query,
        CancellationToken cancellation)
    {
        var items = await dbContext.UserDictionaries
                .Where(d =>
                    d.UserId == query.UserId
                && d.Deleted == null)
                .ToListAsync(cancellation);

        return new ListModel<UserDictionary>
        {
            Data = items,
            Total = items.Count,
        };
    }
}
