using MyDictionary.Domain.Common;

namespace MyDictionary.Domain.Modules.WordForms;

public class WordFormErrors
{
    public static Error NotFound(Guid id) =>
        new("WordForms.NotFound", $"WordForm with id - {id} was not found");
}
