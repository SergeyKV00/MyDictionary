using MyDictionary.Domain.Common;
using MyDictionary.Domain.Interfaces;
using MyDictionary.Domain.UserDictionaryItems;
using MyDictionary.Domain.Users;

namespace MyDictionary.Domain.UserDictionaries;

public class UserDictionary : Entity, IAuditable
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Deleted { get; set; }

    public ICollection<DictionaryItem> Items { get; set; }
}
