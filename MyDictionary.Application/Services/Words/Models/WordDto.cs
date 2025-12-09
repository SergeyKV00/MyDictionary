namespace MyDictionary.Application.Services.DictionaryItems.Models;

public class WordDto
{
    public Guid Id { get; set; }
    public string Term { get; set; }
    public string Meaning { get; set; }
    public int Weight { get; set; }
}
