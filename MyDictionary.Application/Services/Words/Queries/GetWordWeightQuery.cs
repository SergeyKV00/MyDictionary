using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.DictionaryItems;
using System.Linq;

namespace MyDictionary.Application.Services.DictionaryItems.Queries;

public record GetWordWeightQuery(
    Guid DictionaryId, 
    WeightAggregateType WeightAggregate
) : IQuery<int>;

internal class GetWordWeightQueryHandler(IAppDbContext dbContext)
    : IQueryHandler<GetWordWeightQuery, int>
{
    public async Task<Result<int>> Handle(GetWordWeightQuery query, 
        CancellationToken cancellation)
    {
        var queryable = dbContext.Words
            .Where(d => d.Deleted == null)
            .Where(d => d.DictionaryId == query.DictionaryId)
            .Select(d => (int?)d.Weight);

        int? result = query.WeightAggregate switch
        {
            WeightAggregateType.Min => await queryable.MinAsync(cancellation),
            WeightAggregateType.Max => await queryable.MaxAsync(cancellation),
            _ => null
        };

        return result ?? 0;
    }
}
