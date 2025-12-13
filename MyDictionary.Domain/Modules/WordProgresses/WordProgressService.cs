using FSRS.Core.Enums;
using FSRS.Core.Interfaces;
using FSRS.Core.Models;

namespace MyDictionary.Domain.Modules.WordProgresses;

public class WordProgressService : IWordProgressService
{
    private readonly IScheduler _scheduler;

    public WordProgressService(IScheduler scheduler)
    {
        _scheduler = scheduler;
    }

    public void ApplyReview(WordProgress wordProgress, Rating rating)
    {
        var card = new Card(
            state: wordProgress.State,
            stability: wordProgress.Stability,
            difficulty: wordProgress.Difficulty,
            due: wordProgress.NextReview,
            lastReview: wordProgress.LastReview
        );

        var (updateCard, reviewLog) = _scheduler.ReviewCard(card, rating);

        if (updateCard != null)
        {
            wordProgress.State = updateCard.State;
            wordProgress.Stability = updateCard.Stability;
            wordProgress.Difficulty = updateCard.Difficulty;
            wordProgress.NextReview = updateCard.Due;
            wordProgress.LastReview = updateCard.LastReview;
        }
    }
}
