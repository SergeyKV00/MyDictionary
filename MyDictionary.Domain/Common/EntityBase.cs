using MyDictionary.Domain.Interfaces;

namespace MyDictionary.Domain.Common;

public abstract class EntityBase : IEntity
{
    public Guid Id { get; set; }
}
