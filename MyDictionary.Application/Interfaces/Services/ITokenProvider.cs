using MyDictionary.Domain.Modules.Users;

namespace MyDictionary.Application.Interfaces.Services;

public interface ITokenProvider
{
    public string Create(User user);
}
