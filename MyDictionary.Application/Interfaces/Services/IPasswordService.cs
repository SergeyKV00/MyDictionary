namespace MyDictionary.Application.Interfaces.Services;

public interface IPasswordService
{
    public string GenerateHash(string password);

    public bool Verify(string password, string passwordHash);
}
