using FSRS.Constants;
using FSRS.Interfaces;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Interfaces;
using MyDictionary.Domain.Modules.DictionaryItems;
using MyDictionary.Domain.Modules.Users;

namespace MyDictionary.Domain.Modules.WordProgresses;

public class WordProgress : Entity, IAuditable, IFSRSCard
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid DictionaryItemId { get; set; }
    public DictionaryItem? DictionaryItem { get; set; }

    public CardState State { get; set; }
    public double? Stability { get; set; }
    public double? Difficulty { get; set; }
    public DateTime? LastReview { get; set; }
    public DateTime NextReview { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Deleted { get; set; }
}
