using MyDictionary.Domain.Common;
using MyDictionary.Domain.Interfaces;
using MyDictionary.Domain.Modules.DictionaryItems;
using MyDictionary.Domain.Modules.Users;

namespace MyDictionary.Domain.Modules.WordProgresses;

public class WordProgress : Entity, IAuditable
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid DictionaryItemId { get; set; }
    public DictionaryItem? DictionaryItem { get; set; }

    public int Stage { get; set; }
    public double EaseFactor { get; set; }
    public int IntervalDays { get; set; }
    public DateTime NextReview { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Deleted { get; set; }
}
