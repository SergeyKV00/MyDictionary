namespace MyDictionary.Api.Contracts.StudyDecks.Responses;

public record StudyDeckDictionaryResponse(
    Guid DictionaryId,
    string Name,
    bool IsSynchronized
);

public record StudyDeckDetailResponse(
    Guid Id,
    string Name,
    string? Description,
    List<StudyDeckDictionaryResponse> Dictionaries
);
