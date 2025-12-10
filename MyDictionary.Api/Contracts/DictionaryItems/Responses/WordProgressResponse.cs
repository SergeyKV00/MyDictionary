using FSRS.Constants;
using MyDictionary.Domain.Modules.WordProgresses;

namespace MyDictionary.Api.Contracts.DictionaryItems.Responses;

public class WordProgressResponse
{
    public Guid UserId { get; set; }
    public Guid DictionaryItemId { get; set; }
    
    public CardState State { get; set; }
    public DateTime? LastReview {  get; set; }
    public DateTime NextReview { get; set; }
    public int IntervalDays => GetIntervalDays();

    private int GetIntervalDays()
    {
        if (LastReview == null)return 0;

        var interval = (NextReview - LastReview.Value).TotalDays;

        return Math.Max(0, (int)Math.Round(interval));
    }

    public static WordProgressResponse? Map(WordProgress? model)
    {
        if (model == null) return null;

        return new WordProgressResponse
        {
            UserId = model.UserId,
            DictionaryItemId = model.DictionaryItemId,
            State = model.State,
            LastReview = model.LastReview,
            NextReview = model.NextReview,
        };
    }
}
