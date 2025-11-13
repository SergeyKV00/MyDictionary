using MediatR;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Common.Mappings;
using MyDictionary.Application.Interfaces;
using MyDictionary.Application.Services.UserDictionaries.Models;

namespace MyDictionary.Application.Services.UserDictionaries.Queries;

// TODO
public class GetUserDictionaryQueryHandlerOld : IRequestHandler<GetUserDictionaryQueryOld, UserDictionaryDetailsVm>
{
    private readonly IAppDbContext appDbContext;
    public GetUserDictionaryQueryHandlerOld(IAppDbContext appDbContext)
        => this.appDbContext = appDbContext;

    public async Task<UserDictionaryDetailsVm> Handle(GetUserDictionaryQueryOld request,
        CancellationToken cancellationToken)
    {
        var dictionary = await appDbContext.UserDictionaries
            .FirstOrDefaultAsync(dictionary =>
                dictionary.Id == request.Id
            );

        if (dictionary == null)
            throw new Exception(nameof(dictionary));

        return dictionary.ToDetailsView();
    }
}
