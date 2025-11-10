using MediatR;
using MyDictionary.Application.UserDictionaries.Models;

namespace MyDictionary.Application.UserDictionaries.Queries
{
    public class GetUserDictionaryQuery : IRequest<UserDictionaryDetailsVm>
    {
        public Guid Id { get; set; }
    }
}
