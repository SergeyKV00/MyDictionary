using MyDictionary.Domain.Common;
using MyDictionary.Domain.Interfaces;
using MyDictionary.Domain.Modules.Users;

namespace MyDictionary.Domain.Modules.StudyDecks;

public class StudyDeck : Entity, IAuditable
{
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Deleted { get; set; }

    public ICollection<StudyDeckDictionary> Dictionaries { get; set; } = [];
    public ICollection<StudyDeckWord> Words { get; set; } = [];
}
