using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.StudyDecks;

namespace MyDictionary.Application.Services.StudyDecks.Commands;

public record CreateStudyDeckCommand(
    string Name, 
    string? Description
) : ICommand<Guid>;

internal class CreateStudyDeckCommandHandler(
    IAppDbContext appDbContext,
    SessionContext sessionContext
)
    : ICommandHandler<CreateStudyDeckCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateStudyDeckCommand command, 
        CancellationToken cancellation)
    {
        var existsDeck = await appDbContext.StudyDecks
            .AnyAsync(x => 
                x.UserId == sessionContext.UserId &&
                x.Name == command.Name &&
                x.Deleted == null,
                cancellation);
        
        if (existsDeck)
            return StudyDeckErrors.AlreadyExists(command.Name);
        
        var deck = new StudyDeck
        {
            UserId = sessionContext.UserId,
            Name = command.Name,
            Description = command.Description
        };

        await appDbContext.StudyDecks.AddAsync(deck, cancellation);
        await appDbContext.SaveChangesAsync(cancellation);

        return deck.Id;
    }
}

public class CreateStudyDeckCommandValidator 
    : AbstractValidator<CreateStudyDeckCommand>
{
    public CreateStudyDeckCommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty().MaximumLength(500);
        RuleFor(command => command.Description).MaximumLength(2000);
    }
}
