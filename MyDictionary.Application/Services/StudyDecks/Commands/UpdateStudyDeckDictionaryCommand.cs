using FluentValidation;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.StudyDecks;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;

namespace MyDictionary.Application.Services.StudyDecks.Commands;

public record UpdateStudyDeckDictionaryCommand(
    Guid StudyDeckId, 
    Guid DictionaryId, 
    bool IsSynchronized
) : ICommand;

public class UpdateStudyDeckDictionaryCommandValidator 
    : AbstractValidator<UpdateStudyDeckDictionaryCommand>
{
    public UpdateStudyDeckDictionaryCommandValidator()
    {
        RuleFor(x => x.StudyDeckId).NotEmpty();
        RuleFor(x => x.DictionaryId).NotEmpty();
    }
}

public class UpdateStudyDeckDictionaryCommandHandler(IAppDbContext context) 
    : ICommandHandler<UpdateStudyDeckDictionaryCommand>
{
    public async Task<Result> Handle(UpdateStudyDeckDictionaryCommand command, 
        CancellationToken cancellationToken)
    {
        var link = await context.StudyDeckDictionaries
            .FirstOrDefaultAsync(x => 
                x.StudyDeckId == command.StudyDeckId && 
                x.DictionaryId == command.DictionaryId &&
                x.Deleted == null, 
            cancellationToken);

        if (link == null)
            return StudyDeckErrors.DictionaryNotFound(command.DictionaryId);

        link.IsSynchronized = command.IsSynchronized;

        await context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
