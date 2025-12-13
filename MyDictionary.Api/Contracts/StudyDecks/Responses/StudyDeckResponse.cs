using MyDictionary.Domain.Modules.StudyDecks;

namespace MyDictionary.Api.Contracts.StudyDecks.Responses;

public record StudyDeckResponse(
    Guid Id,
    string Name,
    string? Description
)
{
    public static StudyDeckResponse MapFrom(StudyDeck model)
    {
        return new StudyDeckResponse(
            Id: model.Id,
            Name: model.Name,
            Description: model.Description
        );
    }
}
