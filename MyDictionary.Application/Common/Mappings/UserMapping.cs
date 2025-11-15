using MyDictionary.Application.Services.Users.Models;
using MyDictionary.Domain.Modules.Users;

namespace MyDictionary.Application.Common.Mappings;

//TODO Move to Api
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
