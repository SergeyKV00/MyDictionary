using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Common;
using MyDictionary.Application.Interfaces;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.UserDictionaryItems;

namespace MyDictionary.Application.Services.DictionaryItems.Queries;

public record GetDictionaryItemQueryList(
    Guid? DictionaryId,
    string? Term,
    string? Meaning
) : IQuery<ListModel<DictionaryItem>>;

internal class GetDictionaryItemQueryListHandler(IAppDbContext dbContext)
    : IQueryHandler<GetDictionaryItemQueryList, ListModel<DictionaryItem>>
{
    public async Task<Result<ListModel<DictionaryItem>>> Handle(GetDictionaryItemQueryList query, 
        CancellationToken cancellation)
    {
        var dictionaryItems = await dbContext.DictionaryItems
            .Where(d => d.Deleted == null)
            .WhereIfHasKey(query.DictionaryId, d => d.DictionaryId == query.DictionaryId)
            .WhereIfNotEmpty(query.Term, d => d.Term.Contains(query.Term))
            .WhereIfNotEmpty(query.Meaning, d => d.Meaning.Contains(query.Meaning))
            .ToListAsync(cancellation);

        return dictionaryItems.ToListModel();
    }
}