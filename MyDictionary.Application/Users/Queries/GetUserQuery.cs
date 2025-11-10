using MediatR;
using MyDictionary.Application.Users.Models;

namespace MyDictionary.Application.Users.Queries
{
    public class GetUserQuery : IRequest<UserVm>
    {
        public Guid Id { get; set; }
    }
}
