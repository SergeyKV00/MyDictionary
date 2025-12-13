using Microsoft.EntityFrameworkCore;
using MyDictionary.Domain.Modules.DictionaryItemExamples;
using MyDictionary.Domain.Modules.DictionaryItems;
using MyDictionary.Domain.Modules.UserDictionaries;
using MyDictionary.Domain.Modules.Users;
using MyDictionary.Domain.Modules.WordForms;
using MyDictionary.Domain.Modules.WordProgresses;
using MyDictionary.Domain.Modules.StudyDecks;

namespace MyDictionary.Application.Interfaces.Persistence;

public interface IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserDictionary> UserDictionaries { get; set; }
    public DbSet<DictionaryItem> DictionaryItems { get; set; }
    public DbSet<DictionaryItemExample> DictionaryItemExamples { get; set; }
    public DbSet<WordForm> WordForms { get; set; }
    public DbSet<WordProgress> WordProgresses { get; set; }
    public DbSet<StudyDeck> StudyDecks { get; set; }
    public DbSet<StudyDeckDictionary> StudyDeckDictionaries { get; set; }
    public DbSet<StudyDeckWord> StudyDeckWords { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToke);
}
