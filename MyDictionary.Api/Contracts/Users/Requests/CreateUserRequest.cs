namespace MyDictionary.Api.Contracts.User;

public record CreateUserRequest(
    string UserName,
    string Email,
    string Password
);