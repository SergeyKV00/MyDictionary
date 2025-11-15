namespace MyDictionary.Api.Contracts.User;

public record UserCreateRequest(
    string UserName,
    string Email,
    string Password
);

public class UserCreateResponse
{

}
