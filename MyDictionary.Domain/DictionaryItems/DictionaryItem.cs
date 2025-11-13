using MyDictionary.Domain.Common;
using MyDictionary.Domain.Interfaces;
using MyDictionary.Domain.UserDictionaries;

namespace MyDictionary.Domain.UserDictionaryItems;

public class DictionaryItem : Entity, IAuditable
{
    public Guid DictionaryId { get; set; }
    public UserDictionary Dictionary { get; set; }

    public string Term { get; set; }
    public string Meaning { get; set; }
    public int Weight { get; set; }
    public DateTime Created {  get; set; }
    public DateTime? Deleted {  get; set; }
}
