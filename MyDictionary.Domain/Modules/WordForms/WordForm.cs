using MyDictionary.Domain.Common;
using MyDictionary.Domain.Interfaces;
using MyDictionary.Domain.Modules.DictionaryItems;

namespace MyDictionary.Domain.Modules.WordForms;

public class WordForm : Entity, IAuditable
{
    public Guid DictionaryItemId { get; set; }
    public Word? DictionaryItem { get; set; }
    public string? Infinitive { get; set; }
    public string? PastSimple { get; set; }
    public string? PastParticiple { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Deleted { get; set; }
}
