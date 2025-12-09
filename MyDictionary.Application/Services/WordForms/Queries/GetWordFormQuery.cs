using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyDictionary.Application.Interfaces.Messaging;
using MyDictionary.Application.Interfaces.Persistence;
using MyDictionary.Domain.Common;
using MyDictionary.Domain.Modules.WordForms;

namespace MyDictionary.Application.Services.WordForms.Queries;

public record GetWordFormQuery(
    Guid? Id,
    Guid? DictionaryItemId
) : IQuery<WordForm>;

internal class GetWordFormQueryHandler(IAppDbContext dbContext)
    : IQueryHandler<GetWordFormQuery, WordForm>
{
    public async Task<Result<WordForm>> Handle(GetWordFormQuery query,
        CancellationToken cancellation)
    {
        var queryable = dbContext.WordForms.Where(d => d.Deleted == null);

        if (query.Id.HasValue)
            queryable = queryable.Where(d => d.Id == query.Id);
        else
            queryable = queryable.Where(d => d.DictionaryItemId == d.DictionaryItemId);

        var wordForm = await queryable.FirstOrDefaultAsync(cancellation);
        return wordForm;
    }
}

public class GetWordFormQueryValidator : AbstractValidator<GetWordFormQuery>
{
    public GetWordFormQueryValidator()
    {
        RuleFor(x => x)
            .Must(x => x.Id.HasValue || x.DictionaryItemId.HasValue)
            .WithMessage("Either Id or DictionaryId must be provided.");
    }
}
