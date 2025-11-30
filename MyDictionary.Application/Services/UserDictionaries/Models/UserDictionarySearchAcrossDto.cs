
using MyDictionary.Application.Services.DictionaryItems.Models;

namespace MyDictionary.Application.Services.UserDictionaries.Models;

public class UserDictionarySearchAcrossDto
{
    public Guid DictionaryId { get; set; }
    public int MinWeight { get; set; }
    public int MaxWeight { get; set; }
    public List<DictionaryItemDto> Matches { get; set; } = []; 
}
