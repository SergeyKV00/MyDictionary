using MediatR;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Common.Exceptions;
using MyDictionary.Application.Common.Mappings;
using MyDictionary.Application.Interfaces;
using MyDictionary.Application.Services.UserDictionaries.Models;

namespace MyDictionary.Application.Services.UserDictionaries.Queries;

public class GetUserDictionaryQueryHandler : IRequestHandler<GetUserDictionaryQuery, UserDictionaryDetailsVm>
{
    private readonly IAppDbContext appDbContext;
    public GetUserDictionaryQueryHandler(IAppDbContext appDbContext)
        => this.appDbContext = appDbContext;

    public async Task<UserDictionaryDetailsVm> Handle(GetUserDictionaryQuery request,
        CancellationToken cancellationToken)
    {
        var dictionary = await appDbContext.UserDictionaries
            .FirstOrDefaultAsync(dictionary =>
                dictionary.Id == request.Id
            );

        if (dictionary == null)
            throw new NotFoundExceptions(nameof(dictionary), request.Id);

        return dictionary.ToDetailsView();
    }
}
