namespace MyDictionary.Api.Contracts.StudyDecks.Requests;

public record AddDictionaryToStudyDeckRequest(
    Guid StudyDeckId, 
    Guid DictionaryId, 
    bool IsSynchronized
);
public record AddWordsToStudyDeckRequest(
    Guid StudyDeckId, 
    List<Guid> WordIds
);

public record UpdateStudyDeckDictionaryRequest(
    Guid StudyDeckId,
    Guid DictionaryId,
    bool IsSynchronized
);

public record RemoveDictionaryFromStudyDeckRequest(
    Guid StudyDeckId,
    Guid DictionaryId
);

public record RemoveWordsFromStudyDeckRequest(
    Guid StudyDeckId,
    List<Guid> WordIds
);
