using FSRS.Constants;

namespace FSRS.Interfaces;

public interface IFSRSCard
{
    public CardState State { get; set; }
    public double? Stability { get; set; }
    public double? Difficulty { get; set; }
    public DateTime? LastReview { get; set; }
    public DateTime NextReview { get; set; }
}
