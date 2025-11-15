using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Common;
using MyDictionary.Application.Common.Mappings;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Application.Services.UserDictionaries.Models;
using MyDictionary.Domain.Common;

namespace MyDictionary.Application.Services.UserDictionaries.Queries;

public record GetUserDictionaryListQuery(Guid UserId) : IQuery<ListModel<UserDictionaryVm>>;

internal class GetUserDictionaryListQueryHandler(IAppDbContext dbContext)
    : IQueryHandler<GetUserDictionaryListQuery, ListModel<UserDictionaryVm>>

{
    public async Task<Result<ListModel<UserDictionaryVm>>> Handle(GetUserDictionaryListQuery query,
        CancellationToken cancellation)
    {
        var items = await dbContext.UserDictionaries
                .Where(d =>
                    d.UserId == query.UserId
                && d.Deleted == null)
                .ToListAsync(cancellation);

        return new ListModel<UserDictionaryVm>
        {
            Data = items.Select(i => i.ToView()).ToList(),
            Total = items.Count,
        };
    }
}
