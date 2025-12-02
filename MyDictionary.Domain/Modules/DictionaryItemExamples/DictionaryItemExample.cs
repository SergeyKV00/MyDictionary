using MyDictionary.Domain.Common;
using MyDictionary.Domain.Interfaces;
using MyDictionary.Domain.Modules.DictionaryItems;

namespace MyDictionary.Domain.Modules.DictionaryItemExamples;

public class DictionaryItemExample : Entity, IAuditable
{
    public Guid DictionaryItemId { get; set; }
    public DictionaryItem DictionaryItem { get; set; }
    public string Example { get; set; }
    public string? Translation { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Deleted { get; set; }
}
