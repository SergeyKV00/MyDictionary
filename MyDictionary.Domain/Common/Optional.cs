namespace MyDictionary.Domain.Common;

public readonly struct Optional<T>
{
    public bool HasValue { get; }
    public T? Value { get; }

    public Optional(T? value)
    {
        HasValue = true;
        Value = value;
    }

    public static Optional<T> None => new Optional<T>();
}
