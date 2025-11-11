using MyDictionary.Application.Services.Users.Models;
using MyDictionary.Domain.Entities;

namespace MyDictionary.Application.Common.Mappings;

public static class UserMapping
{
    public static UserVm ToView(this User user)
    {
        return new()
        {
            Id = user.Id,
            UserName = user.UserName
        };
    }
}
