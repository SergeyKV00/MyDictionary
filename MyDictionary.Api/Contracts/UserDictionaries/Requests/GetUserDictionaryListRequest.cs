namespace MyDictionary.Api.Contracts.UserDictionaries.Requests;

public record GetUserDictionaryListRequest(
    bool IsIncludeItems = false
);