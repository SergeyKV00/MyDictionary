using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Common;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.DictionaryItemExamples;

namespace MyDictionary.Application.Services.DictionaryItemExamples.Queries;
using ResponseDto = ListModel<DictionaryItemExample>;

public record GetDictionaryItemExampleQueryList(
    Guid? DicitonaryItemId
) : IQuery<ResponseDto>;
internal class GetDictionaryItemExampleQueryListHander(
    IAppDbContext dbContext,
    SessionContext session
) : IQueryHandler<GetDictionaryItemExampleQueryList, ResponseDto>
{
    public async Task<Result<ResponseDto>> Handle(
        GetDictionaryItemExampleQueryList query, 
        CancellationToken cancellation
    )
    {
        var queryable = dbContext.DictionaryItemExamples
            .Where(d => 
                d.DictionaryItem.Dictionary.UserId == session.UserId &&
                d.Deleted == null
            )
            .WhereIfHasKey(query.DicitonaryItemId, d => 
                d.DictionaryItemId == query.DicitonaryItemId
            );

        var examples = await queryable.ToListAsync(cancellation);
        return examples.ToListModel();
    }
}

