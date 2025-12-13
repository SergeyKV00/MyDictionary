namespace MyDictionary.Api.Contracts.StudyDecks.Requests;

public record CreateStudyDeckRequest(
    string Name,
    string? Description
);
