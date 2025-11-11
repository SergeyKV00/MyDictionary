using MyDictionary.Application.Common;

namespace MyDictionary.Application.Services.UserDictionaries.Models;

public class UserDictionaryDetailsVm : ModelBase
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Deleted { get; set; }
}
