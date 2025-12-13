using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Common;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.UserDictionaries;

namespace MyDictionary.Application.Services.UserDictionaries.Queries;

public record GetUserDictionaryListQuery(
    Guid UserId,
    bool IsIncludeItems,
    List<Guid>? Ids = null
) : IQuery<ListModel<UserDictionary>>;

internal class GetUserDictionaryListQueryHandler(IAppDbContext dbContext)
    : IQueryHandler<GetUserDictionaryListQuery, ListModel<UserDictionary>>

{
    public async Task<Result<ListModel<UserDictionary>>> Handle(GetUserDictionaryListQuery query,
        CancellationToken cancellation)
    {
        var queryable = dbContext.UserDictionaries
            .Where(d =>
                d.UserId == query.UserId &&
                d.Deleted == null)
            .WhereIf(query.Ids?.Count > 0, x => query.Ids!.Contains(x.Id));

        if (query.IsIncludeItems)
            queryable = queryable.Include(d => d.Items);

        var items = await queryable.ToListAsync(cancellation);

        return items.ToListModel();
    }
}
