using FluentValidation;

namespace MyDictionary.Application.UserDictionaries.Commands
{
    public class CreateUserDictionaryCommandValidator : AbstractValidator<CreateUserDictionaryCommand>
    {
        public CreateUserDictionaryCommandValidator()
        {
            RuleFor(command => command.UserId).NotEqual(Guid.Empty);
            RuleFor(command => command.Name).NotEmpty();
        }
    }
}
