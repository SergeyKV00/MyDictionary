using MediatR;
using MyDictionary.Application.Services.Users.Models;

namespace MyDictionary.Application.Services.Users.Queries;

public class GetUserQuery : IRequest<UserVm>
{
    public Guid Id { get; set; }
}
