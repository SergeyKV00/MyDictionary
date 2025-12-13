using MyDictionary.Domain.Common;
using MyDictionary.Domain.Interfaces;
using MyDictionary.Domain.Modules.UserDictionaries;

namespace MyDictionary.Domain.Modules.StudyDecks;

public class StudyDeckDictionary : Entity, IAuditable
{
    public Guid StudyDeckId { get; set; }
    public StudyDeck? StudyDeck { get; set; }

    public Guid DictionaryId { get; set; }
    public UserDictionary? Dictionary { get; set; }

    public bool IsSynchronized { get; set; }

    public DateTime Created { get; set; }
    public DateTime? Deleted { get; set; }  
}
