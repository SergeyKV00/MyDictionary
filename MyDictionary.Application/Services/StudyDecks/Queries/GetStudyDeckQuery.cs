using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Common;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.StudyDecks;

namespace MyDictionary.Application.Services.StudyDecks.Queries;

public record GetStudyDeckQuery(Guid Id) : IQuery<StudyDeck>;

public class GetStudyDeckQueryHandler(IAppDbContext context) 
    : IQueryHandler<GetStudyDeckQuery, StudyDeck>
{
    public async Task<Result<StudyDeck>> Handle(GetStudyDeckQuery query, 
        CancellationToken cancellationToken)
    {
        var deck = await context.StudyDecks
            .Include(x => x.Dictionaries)
            .ThenInclude(x => x.Dictionary)
            .FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

        if (deck == null)
            return Result.Failure<StudyDeck>(StudyDeckErrors.NotFound(query.Id));

        return Result.Success(deck);
    }
}
