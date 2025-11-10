using MediatR;
using MyDictionary.Application.Common;
using MyDictionary.Application.UserDictionaries.Models;

namespace MyDictionary.Application.UserDictionaries.Queries
{
    public class GetUserDictionaryListQuery : IRequest<ListModel<UserDictionaryVm>>
    {
        public Guid UserId { get; set; }
    }
}
