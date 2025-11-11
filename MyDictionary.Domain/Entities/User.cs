using MyDictionary.Domain.Common;
using MyDictionary.Domain.Interfaces;

namespace MyDictionary.Domain.Entities;

public class User : EntityBase, IAuditable
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Deleted { get; set; }

    public ICollection<UserDictionary> UserDictionaries { get; set; } = [];
}
