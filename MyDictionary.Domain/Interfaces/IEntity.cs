namespace MyDictionary.Domain.Interfaces
{
    public interface IEntity<TIdentifier>
    {
        TIdentifier Id { get; set; }
    }
}
