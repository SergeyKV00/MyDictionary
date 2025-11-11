using FluentValidation;
using MediatR;
using MyDictionary.Application.Interfaces;
using MyDictionary.Domain.Entities;

namespace MyDictionary.Application.Services.UserDictionaries.Commands;

public record CreateUserDictionaryCommand(Guid UserId, string Name) : IRequest<Guid>
{
    public class Handler(IAppDbContext appDbContext) 
        : IRequestHandler<CreateUserDictionaryCommand, Guid>
    {
        public async Task<Guid> Handle(CreateUserDictionaryCommand request, 
            CancellationToken cancellationToken)
        {
            var dictionary = new UserDictionary()
            {
                UserId = request.UserId,
                Name = request.Name
            };

            await appDbContext.UserDictionaries.AddAsync(dictionary, cancellationToken);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return dictionary.Id;
        }
    }

    public class Validator : AbstractValidator<CreateUserDictionaryCommand>
    {
        public Validator()
        {
            RuleFor(command => command.UserId).NotEqual(Guid.Empty);
            RuleFor(command => command.Name).NotEmpty();
        }
    }

}
