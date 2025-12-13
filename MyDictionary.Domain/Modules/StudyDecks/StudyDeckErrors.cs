using MyDictionary.Domain.Common;

namespace MyDictionary.Domain.Modules.StudyDecks;

public static class StudyDeckErrors
{
    public static Error AlreadyExists(string name) 
        => new("StudyDeck.AlreadyExists", $"StudyDeck with name '{name}' already exists");
    public static Error NotFound(Guid id) 
        => new("StudyDeck.NotFound", $"StudyDeck with id '{id}' not found");
    public static Error DictionaryAlreadyExists(Guid dictionaryId)
        => new("StudyDeck.DictionaryAlreadyExists", $"Dictionary with id '{dictionaryId}' already exists in this deck");
    public static Error DictionaryNotFound(Guid dictionaryId)
        => new("StudyDeck.DictionaryNotFound", $"Dictionary with id '{dictionaryId}' not found in this deck");
}
