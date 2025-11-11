using MediatR;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Common;
using MyDictionary.Application.Common.Mappings;
using MyDictionary.Application.Interfaces;
using MyDictionary.Application.Services.UserDictionaries.Models;

namespace MyDictionary.Application.Services.UserDictionaries.Queries;

public record GetUserDictionaryListQuery(Guid UserId) : IRequest<ListModel<UserDictionaryVm>>
{
    public class Handler(IAppDbContext appDbContext) 
        : IRequestHandler<GetUserDictionaryListQuery, ListModel<UserDictionaryVm>>
    {
        public async Task<ListModel<UserDictionaryVm>> Handle(GetUserDictionaryListQuery request,
            CancellationToken cancellationToken)
        {
            var items = await appDbContext.UserDictionaries
                .Where(d => d.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            return new ListModel<UserDictionaryVm>
            {
                Data = items.Select(i => i.ToView()).ToList(),
                Total = items.Count,
            };
        }
    }
}
