namespace MyDictionary.Domain.Modules.WordProgresses;

public interface IWordProgressService
{
    void ApplyReview(WordProgress wordProgress, ProgressQuality rating);
}
