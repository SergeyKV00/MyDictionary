using MyDictionary.Domain.Common;
using MyDictionary.Domain.Interfaces;
using MyDictionary.Domain.Modules.UserDictionaries;
using MyDictionary.Domain.Modules.WordProgresses;

namespace MyDictionary.Domain.Modules.Users;

public class User : Entity, IAuditable
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Deleted { get; set; }

    public ICollection<UserDictionary> Dictionaries { get; set; }
    public ICollection<StudyDecks.StudyDeck> StudyDecks { get; set; } = [];
    public ICollection<WordProgress> WordProgresses { get; set; }
}
