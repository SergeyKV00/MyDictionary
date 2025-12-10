using FSRS.Constants;
using FSRS.Interfaces;

namespace MyDictionary.Domain.Modules.WordProgresses;

public class WordProgressService : IWordProgressService
{
    private readonly IScheduler scheduler;

    public WordProgressService(IScheduler scheduler)
    {
        this.scheduler = scheduler;
    }

    public void ApplyReview(WordProgress wordProgress, Rating rating)
    {
        scheduler.Review(wordProgress, rating, wordProgress.NextReview);
    }

    //public void ApplyReview(WordProgress wp, Rating quality)
    //{
    //    wp.EaseFactor = Math.Max(1.3,
    //        wp.EaseFactor + (0.1 - (5 - (int)quality) * (0.08 + (5 - (int)quality) * 0.02)));

    //    if (quality == Rating.Forgot)
    //        wp.Stage = 0;

    //    wp.IntervalDays = wp.Stage switch
    //    {
    //        0 => 1,
    //        1 => 3,
    //        _ => (int)Math.Round(wp.IntervalDays * wp.EaseFactor),
    //    };

    //    wp.Stage++;
    //    wp.NextReview = DateTime.Today.AddDays(wp.IntervalDays).AddHours(6);
    //}
}
