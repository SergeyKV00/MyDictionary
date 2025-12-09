using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Application.Services.DictionaryItems.Models;
using MyDictionary.Application.Services.UserDictionaries.Models;
using MyDictionary.Domain;
using MyDictionary.Domain.Common;

namespace MyDictionary.Application.Services.UserDictionaries.Queries;

public record SearchAcrosssUserDictionariesQueryList(string? Term) 
    : IQuery<IList<UserDictionarySearchAcrossDto>>;
internal class SearchAcrosssUserDictionariesQueryListHandler(
    IAppDbContext dbContex,
    SessionContext session) 
    : IQueryHandler<SearchAcrosssUserDictionariesQueryList, IList<UserDictionarySearchAcrossDto>>
{
    public async Task<Result<IList<UserDictionarySearchAcrossDto>>> Handle(
        SearchAcrosssUserDictionariesQueryList query,
        CancellationToken cancellation)
    {
        if (string.IsNullOrWhiteSpace(query.Term))
            return new List<UserDictionarySearchAcrossDto>();

        var dictionaries = await dbContex.UserDictionaries
            .AsNoTracking()
            .Where(ud =>
                ud.UserId == session.UserId &&
                ud.Deleted == null)
            .Select(ud => new UserDictionarySearchAcrossDto
            {
                DictionaryId = ud.Id,
                Matches = ud.Items
                    .Where(di =>
                        di.Deleted == null &&
                        di.Term.Contains(query.Term))
                    .OrderByDescending(di => di.Weight)
                    .Take(3)
                    .Select(di => new WordDto
                    {
                        Id = di.Id,
                        Term = di.Term,
                        Meaning = di.Meaning,
                        Weight = di.Weight
                    })
                    .ToList()
            })
            .ToListAsync(cancellation);

        await FillWeights(dictionaries, cancellation);

        return dictionaries;
    }

    public async Task FillWeights(IEnumerable<UserDictionarySearchAcrossDto> dictionaries,
        CancellationToken cancellation)
    {
        var dictionaryWithMatchesIds = dictionaries
            .Where(r => r.Matches.Count > 0)
            .Select(r => r.DictionaryId);

        var weights = await dbContex.Words
            .AsNoTracking()
            .Where(di =>
                dictionaryWithMatchesIds.Contains(di.DictionaryId) &&
                di.Deleted == null)
            .GroupBy(di => di.DictionaryId)
            .Select(g => new
            {
                g.Key,
                MinWeight = g.Min(d => d.Weight),
                MaxWeight = g.Max(d => d.Weight)
            })
            .ToDictionaryAsync(g => g.Key, g => g, cancellation);

        foreach (var dictionary in dictionaries)
        {
            var weight = weights.GetValueOrDefault(dictionary.DictionaryId);
            if (weight != null)
            {
                dictionary.MinWeight = weight.MinWeight;
                dictionary.MaxWeight = weight.MaxWeight;
            }
        }
    }
}
