using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Common;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.StudyDecks;

namespace MyDictionary.Application.Services.StudyDecks.Queries;

public record GetStudyDeckWordsQuery(Guid StudyDeckId, 
    int Page, 
    int PageSize, 
    string? SortField = null, 
    string? SortOrder = null) 
: IQueryPages<ListModel<StudyDeckWord>>;

public class GetStudyDeckWordsQueryHandler(IAppDbContext context) 
    : IQueryHandler<GetStudyDeckWordsQuery, ListModel<StudyDeckWord>>
{
    public async Task<Result<ListModel<StudyDeckWord>>> Handle(GetStudyDeckWordsQuery query,
        CancellationToken cancellationToken)
    {
         var queryable = context.StudyDeckWords
             .Include(x => x.DictionaryItem)
             .Where(x => x.StudyDeckId == query.StudyDeckId && x.Deleted == null);

         queryable = queryable.ApplySort(query.SortField, query.SortOrder);
         var pagedList = await queryable.CreateAsync(query, cancellationToken);
         
         return Result.Success(pagedList);
    }
}
