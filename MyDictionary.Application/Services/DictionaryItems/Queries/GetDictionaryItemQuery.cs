using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.DictionaryItems;

namespace MyDictionary.Application.Services.DictionaryItems.Queries;

public record GetDictionaryItemQuery(
    Guid Id,
    bool IsIncludeExample = false,
    bool IsIncludeWordForm = false
) : IQuery<DictionaryItem>
{
    public class Handler(IAppDbContext dbContext)
        : IQueryHandler<GetDictionaryItemQuery, DictionaryItem>
    {
        public async Task<Result<DictionaryItem>> Handle(GetDictionaryItemQuery query,
            CancellationToken cancellation)
        {
            var queryable = dbContext.DictionaryItems
                .AsNoTracking()
                .AsQueryable();

            if (query.IsIncludeExample)
                queryable = queryable.Include(d =>
                    d.Examples.Where(d => d.Deleted == null)
                );

            if (query.IsIncludeWordForm)
                queryable = queryable.Include(d => d.WordForm);

            var item = await queryable
                .FirstOrDefaultAsync(d => d.Id == query.Id, cancellation);

            if (item == null)
                return DictionaryItemErrors.NotFound(query.Id);

            return item;
        }
    }
}


