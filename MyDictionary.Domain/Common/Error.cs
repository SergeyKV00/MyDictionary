namespace MyDictionary.Domain.Common;

public record Error(string Code, string Description, object? Metadata = null)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "Null value was provided");

    public static implicit operator Result(Error error) => Result.Failure(error);

    public Result ToResult() => Result.Failure(this);
    public static Error Validation(string code, string description, object errors)
    => new(code, description, errors);
}