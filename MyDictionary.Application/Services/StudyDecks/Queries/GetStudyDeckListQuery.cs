using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Common;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.StudyDecks;

namespace MyDictionary.Application.Services.StudyDecks.Queries;

public record GetStudyDeckListQuery() : IQuery<ListModel<StudyDeck>>;

internal class GetStudyDeckListQueryHandler(
    IAppDbContext dbContext,
    SessionContext sessionContext
    )
    : IQueryHandler<GetStudyDeckListQuery, ListModel<StudyDeck>>
{
    public async Task<Result<ListModel<StudyDeck>>> Handle(GetStudyDeckListQuery query,
        CancellationToken cancellation)
    {
        var items = await dbContext.StudyDecks
            .Where(d =>
                d.UserId == sessionContext.UserId &&
                d.Deleted == null)
            .OrderByDescending(x => x.Created)
            .ToListAsync(cancellation);

        return items.ToListModel();
    }
}
