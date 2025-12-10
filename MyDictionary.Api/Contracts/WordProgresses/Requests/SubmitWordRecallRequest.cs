using MyDictionary.Domain.Modules.WordProgresses;

namespace MyDictionary.Api.Contracts.WordProgresses.Requests;

public record SubmitWordRecallRequest(Guid DictiomaryItemId, ProgressQuality Rating);
