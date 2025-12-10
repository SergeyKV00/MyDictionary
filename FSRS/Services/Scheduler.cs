using FSRS.Constants;
using FSRS.Interfaces;

namespace FSRS.Services;

public class Scheduler : IScheduler
{
    private readonly double[] parameters = FsrsConstants.DefaultParameters;
    public IFSRSCard Review(IFSRSCard card, Rating rating, DateTime? reviewDate = null)
    {
        reviewDate ??= DateTime.UtcNow;

        if (card.LastReview == null) 
            return FirstReview(card, rating, reviewDate.Value);

        return NextReview(card, rating, reviewDate.Value);
    }

    private IFSRSCard FirstReview(IFSRSCard card, Rating rating, DateTime now)
    {
        if (rating == Rating.Again)
            card.State = CardState.Learning;

        double difficulty = parameters[4];
        double stability = rating == Rating.Again ? 0 : parameters[0] + parameters[1];

        card.Difficulty = difficulty;
        card.Stability = stability;
        card.LastReview = now;
        card.NextReview = now.AddMinutes(10);

        return card;
    }

    private IFSRSCard NextReview(IFSRSCard card, Rating rating, DateTime now)
    {
        card.State = CardState.Review;

        var intervalDays = (now - card.LastReview!.Value).TotalDays;
        var retrievability = Math.Exp(-intervalDays / card.Stability!.Value);

        card.Difficulty = UpdateDifficulty(card.Difficulty!.Value, rating);

        if (rating == Rating.Again)
            card.Stability = UpdateStabilityFail(card.Stability!.Value, card.Difficulty!.Value);
        else
            card.Stability = UpdateStabilitySuccess(card.Stability!.Value, card.Difficulty!.Value, rating);

        card.LastReview = now;
        card.NextReview = now.AddDays(card.Stability!.Value);

        return card;
    }

    private double UpdateDifficulty(double oldD, Rating rating)
    {
        double r = (int)rating - 3;
        double newD = oldD + parameters[7] * r;

        return Math.Clamp(newD, 1.0, 10.0);
    }

    private double UpdateStabilitySuccess(double oldS, double difficulty, Rating rating)
    {
        double hardPenalty = difficulty / 10.0;
        double ratingFactor = (int)rating / 4.0;

        double newS = oldS * (1 + parameters[8] * (1 - hardPenalty) * ratingFactor);

        return Math.Max(0.1, newS);
    }

    private double UpdateStabilityFail(double oldS, double difficulty)
    {
        double factor = Math.Exp(-difficulty * parameters[9]);
        double newS = oldS * factor;

        return Math.Max(0.1, newS);
    }
}
