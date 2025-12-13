using FSRS.Core.Enums;
using MyDictionary.Domain.Modules.WordProgresses;

namespace MyDictionary.Api.Contracts.WordProgresses.Responses;

public class WordProgressResponse
{
    public Guid UserId { get; set; }
    public Guid DictionaryItemId { get; set; }
    
    public State State { get; set; }
    public DateTime? LastReview {  get; set; }
    public DateTime NextReview { get; set; }
    public double? Stability { get; set; }
    public double? Difficulty { get; set; }
    public int Retention => GetRetention();

    private int GetRetention()
    {
        double t = (DateTime.UtcNow.AddDays(5) - (LastReview ?? DateTime.UtcNow)).TotalDays;
        double S = Stability ?? 0;
        double retention = Math.Pow(2, -t / S);

        double percent = retention * 100;
        return Convert.ToInt32(Math.Clamp(percent, 0, 100));
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
            Stability = model.Stability,
            Difficulty = model.Difficulty,
        };
    }
}
