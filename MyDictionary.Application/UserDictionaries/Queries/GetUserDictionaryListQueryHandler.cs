using MediatR;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Common;
using MyDictionary.Application.Common.Mappings;
using MyDictionary.Application.Interfaces;
using MyDictionary.Application.UserDictionaries.Models;

namespace MyDictionary.Application.UserDictionaries.Queries
{
    public class GetUserDictionaryListQueryHandler
        : IRequestHandler<GetUserDictionaryListQuery, ListModel<UserDictionaryVm>>
    {
        private readonly IAppDbContext appDbContext;

        public GetUserDictionaryListQueryHandler(IAppDbContext appDbContext)
            => this.appDbContext = appDbContext;
        
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
