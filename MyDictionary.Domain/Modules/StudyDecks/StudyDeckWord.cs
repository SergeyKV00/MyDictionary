using MyDictionary.Domain.Common;
using MyDictionary.Domain.Interfaces;
using MyDictionary.Domain.Modules.DictionaryItems;

namespace MyDictionary.Domain.Modules.StudyDecks;

public class StudyDeckWord : Entity, IAuditable
{
    public Guid StudyDeckId { get; set; }
    public StudyDeck? StudyDeck { get; set; }

    public Guid DictionaryItemId { get; set; }
    public DictionaryItem? DictionaryItem { get; set; }

    public DateTime Created { get; set; }
    public DateTime? Deleted { get; set; }
}
