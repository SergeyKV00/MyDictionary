using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Common;
public record GetWordCountQuery(Guid DictionaryId) : IQuery<int>;

internal class GetWordCountQueryHandler(IAppDbContext dbContext)
    : IQueryHandler<GetWordCountQuery, int>
{
    public async Task<Result<int>> Handle(GetWordCountQuery query,
        CancellationToken cancellation)
    {
        var count = await dbContext.Words
            .Where(d => d.Deleted == null)
            .Where(d => d.DictionaryId == query.DictionaryId)
            .CountAsync(cancellation);

        return count;
    }
}
