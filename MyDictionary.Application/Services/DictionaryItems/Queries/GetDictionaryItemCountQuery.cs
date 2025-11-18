using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Common;
public record GetDictionaryItemCountQuery(Guid DictionaryId) : IQuery<int>;

internal class GetDictionaryItemCountQueryHandler(IAppDbContext dbContext)
    : IQueryHandler<GetDictionaryItemCountQuery, int>
{
    public async Task<Result<int>> Handle(GetDictionaryItemCountQuery query,
        CancellationToken cancellation)
    {
        var count = await dbContext.DictionaryItems
            .Where(d => d.Deleted == null)
            .Where(d => d.DictionaryId == query.DictionaryId)
            .CountAsync(cancellation);

        return count;
    }
}
