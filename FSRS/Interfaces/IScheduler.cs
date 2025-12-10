using FSRS.Constants;

namespace FSRS.Interfaces;

public interface IScheduler
{
    public IFSRSCard Review(IFSRSCard card, Rating rating, DateTime? reviewDate = null);
}
