using MyDictionary.Domain.Modules.DictionaryItems;
using MyDictionary.Domain.Modules.WordProgresses;

namespace MyDictionary.Api.Contracts.DictionaryItems.Responses;

public class WordProgressResponse
{
    public Guid UserId { get; set; }
    public Guid DictionaryItemId { get; set; }

    public int Stage { get; set; }
    public double EaseFactor { get; set; }
    public int IntervalDays { get; set; }
    public DateTime NextReview { get; set; }

    public static WordProgressResponse? Map(WordProgress? model)
    {
        if (model == null) return null;

        return new WordProgressResponse
        {
            UserId = model.UserId,
            DictionaryItemId = model.DictionaryItemId,
            Stage = model.Stage,
            EaseFactor = model.EaseFactor,
            IntervalDays = model.IntervalDays,
            NextReview = model.NextReview
        };
    }
}
