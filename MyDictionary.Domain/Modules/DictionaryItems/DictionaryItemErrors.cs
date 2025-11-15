using MyDictionary.Domain.Common;

namespace MyDictionary.Domain.Modules.DictionaryItems;

public static class DictionaryItemErrors
{
    public static Error NotFound(Guid id) =>
        new("DictionaryItems.NotFound", $"DictionaryItem with id - {id} was not found");
}
