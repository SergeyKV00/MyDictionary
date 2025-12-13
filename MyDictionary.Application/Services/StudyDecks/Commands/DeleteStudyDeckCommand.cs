using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.StudyDecks;

namespace MyDictionary.Application.Services.StudyDecks.Commands;

public record DeleteStudyDeckCommand(Guid Id) : ICommand;

internal class DeleteStudyDeckCommandHandler(IAppDbContext appDbContext)
    : ICommandHandler<DeleteStudyDeckCommand>
{
    public async Task<Result> Handle(DeleteStudyDeckCommand command, CancellationToken cancellation)
    {
        var deck = await appDbContext.StudyDecks
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellation);

        if (deck == null)
            return StudyDeckErrors.NotFound(command.Id);

        deck.Deleted = DateTime.UtcNow;
        
        await appDbContext.SaveChangesAsync(cancellation);
        return Result.Success();
    }
}

public class DeleteStudyDeckCommandValidator : AbstractValidator<DeleteStudyDeckCommand>
{
    public DeleteStudyDeckCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}
