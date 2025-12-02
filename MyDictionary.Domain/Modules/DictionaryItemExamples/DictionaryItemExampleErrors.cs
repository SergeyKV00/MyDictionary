using MyDictionary.Domain.Common;

namespace MyDictionary.Domain.Modules.DictionaryItemExamples;

public static class DictionaryItemExampleErrors
{
    public static Error NotFound(Guid id) =>
        new("DictionaryItemExamples.NotFound",  $"DictionaryItemExample with id - {id} was not found");
}
