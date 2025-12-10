using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Common;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.DictionaryItems;
using MyDictionary.Domain.Modules.WordProgresses;

namespace MyDictionary.Application.Services.DictionaryItems.Queries;

public record GetDictionaryItemQueryList(
    Guid? DictionaryId,
    string? Term,
    string? Meaning,
    string? SortField,
    string? SortOrder,
    int Page,
    int PageSize,
    bool IsIncludeWordProgress = false
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


        queryable = queryable.ApplySort(query.SortField, query.SortOrder);
        var items = await queryable.CreateAsync(query, cancellation);

        await FillDetails(items, query, cancellation);
        return items;
    }

    private async Task FillDetails(
        ListModel<DictionaryItem> items, 
        GetDictionaryItemQueryList query,
        CancellationToken cancellation)
    {
        if (items?.Data == null) return;

        var wordProgresses = new Dictionary<Guid, WordProgress>();

        if (query.IsIncludeWordProgress)
        {
            var wordProgressIds = items.Data.Select(d => d.Id);
            wordProgresses = await dbContext.WordProgresses
                .Where(d => 
                    wordProgressIds.Contains(d.DictionaryItemId) &&
                    d.Deleted == null
                )
                .ToDictionaryAsync(x => x.DictionaryItemId, x => x, cancellation);
        }

        foreach ( var item in items.Data)
        {
            item.WordProgress = wordProgresses.GetValueOrDefault(item.Id);
        }
    }
}