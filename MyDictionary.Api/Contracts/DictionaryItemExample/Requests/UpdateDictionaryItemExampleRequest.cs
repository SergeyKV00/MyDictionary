using MyDictionary.Domain.Common;

namespace MyDictionary.Api.Contracts.DictionaryItemExample.Requests;

public record UpdateDictionaryItemExampleRequest(
    Guid Id,
    string Example,
    Optional<string?> Translation
);
