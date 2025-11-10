using MediatR;
using MyDictionary.Application.Interfaces;
using MyDictionary.Domain.Entities;

namespace MyDictionary.Application.UserDictionaries.Commands
{
    public class CreateUserDictionaryCommandHandler : IRequestHandler<CreateUserDictionaryCommand, Guid>
    {
        private readonly IAppDbContext appDbContext;
        public CreateUserDictionaryCommandHandler(IAppDbContext appDbContext) 
            => this.appDbContext = appDbContext;

        public async Task<Guid> Handle(CreateUserDictionaryCommand request, CancellationToken cancellationToken)
        {
            var dictionary = new UserDictionary()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Name = request.Name
            };

            await appDbContext.UserDictionaries.AddAsync(dictionary, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return dictionary.Id;
        }
    }
}
