namespace MyDictionary.Api.Abstracts;

public record SuccessResponse<T>(
    T Value,
    bool IsSuccess
);
