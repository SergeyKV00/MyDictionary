using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.DictionaryItems;

namespace MyDictionary.Application.Services.DictionaryItems.Queries;

public record GetWordQuery(
    Guid Id,
    bool IsIncludeExample = false,
    bool IsIncludeWordForm = false
) : IQuery<Word>
{
    public class Handler(IAppDbContext dbContext)
        : IQueryHandler<GetWordQuery, Word>
    {
        public async Task<Result<Word>> Handle(GetWordQuery query,
            CancellationToken cancellation)
        {
            var queryable = dbContext.Words
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
                return WordErrors.NotFound(query.Id);

            return item;
        }
    }
}


