using Microsoft.VisualBasic;

namespace MyDictionary.Domain.Modules.WordProgresses;

public class WordProgressService : IWordProgressService
{
    public void ApplyReview(WordProgress wp, ProgressQuality quality)
    {
        wp.EaseFactor = Math.Max(1.3,
            wp.EaseFactor + (0.1 - (5 - (int)quality) * (0.08 + (5 - (int)quality) * 0.02)));

        if (quality == ProgressQuality.Forgot)
            wp.Stage = 0;

        wp.IntervalDays = wp.Stage switch
        {
            0 => 1,
            1 => 3,
            _ => (int)Math.Round(wp.IntervalDays * wp.EaseFactor),
        };

        wp.Stage++;
        wp.NextReview = DateTime.Today.AddDays(wp.IntervalDays).AddHours(6);
    }
}
