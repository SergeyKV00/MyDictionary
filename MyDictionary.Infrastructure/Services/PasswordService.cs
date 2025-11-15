using MyDictionary.Application.Interfaces.Services;

namespace MyDictionary.Infrastructure.Services;

public sealed class PasswordService : IPasswordService
{
    public string GenerateHash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
