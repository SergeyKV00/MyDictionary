using MediatR;

namespace MyDictionary.Application.UserDictionaries.Commands
{
    public class CreateUserDictionaryCommand : IRequest<Guid>
    {
        public Guid UserId { get ; set; }
        public string Name { get; set; }
    }
}
