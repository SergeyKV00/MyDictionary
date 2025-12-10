using MyDictionary.Domain.Common;

namespace MyDictionary.Domain.Modules.WordProgresses;

public class WordProgressErrors
{
    public static Error NotFound() =>
        new("WordProgresses.NotFound", $"WordProgress not found");
}
