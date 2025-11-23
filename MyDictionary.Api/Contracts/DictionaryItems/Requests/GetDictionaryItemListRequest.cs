using MyDictionary.Api.Abstracts;

namespace MyDictionary.Api.Contracts.DictionaryItems;

public record GetDictionaryItemListRequest(
    Guid? DictionaryId,
    string? Term,
    string? Meaning,
    int Offset,
    int Limit = int.MaxValue
) : PageRequest(Offset, Limit);