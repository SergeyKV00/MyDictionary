namespace MyDictionary.Api.Contracts.StudyDecks.Requests;

public record GetStudyDeckRequest(Guid Id);

public record GetStudyDeckWordsRequest(
    Guid StudyDeckId, 
    int Page, 
    int PageSize, 
    string? SortField = null, 
    string? SortOrder = null
);
