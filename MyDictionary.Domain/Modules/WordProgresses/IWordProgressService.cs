using FSRS.Core.Enums;

namespace MyDictionary.Domain.Modules.WordProgresses;

public interface IWordProgressService
{
    void ApplyReview(WordProgress wordProgress, Rating rating);
}
