using FSRS.Constants;

namespace MyDictionary.Api.Contracts.WordProgresses.Requests;

public record SubmitWordRecallRequest(Guid DictiomaryItemId, Rating Rating);
