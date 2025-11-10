using MyDictionary.Domain.Common;
using MyDictionary.Domain.Interfaces;

namespace MyDictionary.Domain.Entities
{
    public class UserDictionary : EntityBase, IAuditable
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
