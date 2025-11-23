using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Common;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.DictionaryItems;

namespace MyDictionary.Application.Services.DictionaryItems.Queries;

public record GetDictionaryItemQueryList(
    Guid? DictionaryId,
    string? Term,
    string? Meaning,
    int Offset,
    int Limit
) : IQueryPages<ListModel<DictionaryItem>>;

internal class GetDictionaryItemQueryListHandler(IAppDbContext dbContext, SessionContext session)
    : IQueryHandler<GetDictionaryItemQueryList, ListModel<DictionaryItem>>
{
    public async Task<Result<ListModel<DictionaryItem>>> Handle(GetDictionaryItemQueryList query, 
        CancellationToken cancellation)
    {
        var queryable = dbContext.DictionaryItems
            .Where(d => d.Deleted == null)
            .Where(d => d.Dictionary.UserId == session.UserId)
            .WhereIfHasKey(query.DictionaryId, d => d.DictionaryId == query.DictionaryId)
            .WhereIfNotEmpty(query.Term, d => d.Term.Contains(query.Term))
            .WhereIfNotEmpty(query.Meaning, d => d.Meaning.Contains(query.Meaning));

        var totalCount = await queryable.CountAsync(cancellation);

        var dictionaryItems = await queryable
            .OrderByDescending(d => d.Weight)
            .Skip(query.Offset)
            .Take(query.Limit)
            .ToListAsync(cancellation);

        return dictionaryItems.ToListModel(totalCount);
    }
}