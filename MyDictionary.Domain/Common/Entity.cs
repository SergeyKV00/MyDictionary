using MyDictionary.Domain.Interfaces;

namespace MyDictionary.Domain.Common;

public abstract class Entity : IEntity
{
    public Guid Id { get; set; }
}
