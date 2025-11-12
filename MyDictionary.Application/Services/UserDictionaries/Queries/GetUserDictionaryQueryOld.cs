using MediatR;
using MyDictionary.Application.Services.UserDictionaries.Models;

namespace MyDictionary.Application.Services.UserDictionaries.Queries;

public class GetUserDictionaryQueryOld : IRequest<UserDictionaryDetailsVm>
{
    public Guid Id { get; set; }
}
