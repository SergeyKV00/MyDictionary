using MyDictionary.Application.UserDictionaries.Models;
using MyDictionary.Domain.Entities;

namespace MyDictionary.Application.Common.Mappings
{
    public static class UserDictionaryMapping
    {
        public static UserDictionaryVm ToView(this UserDictionary entity)
        {
            return new()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public static UserDictionaryDetailsVm ToDetailsView(this UserDictionary entity)
        {
            return new()
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Name = entity.Name,
                Created = entity.Created,
                Deleted = entity.Deleted
            };
        }
    }
}
