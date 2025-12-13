namespace MyDictionary.Api.Contracts.StudyDecks.Responses;

public record StudyDeckWordResponse(
    Guid Id,
    string Term,
    string? Meaning
);
