using MyDictionary.Application.Common;

namespace MyDictionary.Application.UserDictionaries.Models
{
    public class UserDictionaryDetailsVm : ModelBase
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Deleted { get; set; }
    }
}
