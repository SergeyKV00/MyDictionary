using MyDictionary.Domain.Interfaces;

namespace MyDictionary.Domain.Common
{
    public class EntityBase : IEntity<Guid>
    {
        public Guid Id { get; set; }
    }
}
