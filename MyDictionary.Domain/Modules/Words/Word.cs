using MyDictionary.Domain.Common;
using MyDictionary.Domain.Interfaces;
using MyDictionary.Domain.Modules.DictionaryItemExamples;
using MyDictionary.Domain.Modules.UserDictionaries;
using MyDictionary.Domain.Modules.WordForms;

namespace MyDictionary.Domain.Modules.DictionaryItems;

public class Word : Entity, IAuditable
{
    public Guid DictionaryId { get; set; }
    public UserDictionary Dictionary { get; set; }
    public string Term { get; set; }
    public string Meaning { get; set; }
    public int Weight { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Deleted { get; set; }

    public ICollection<DictionaryItemExample> Examples { get; set; } = [];
    public WordForm? WordForm { get; set; }
}
