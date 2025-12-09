using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDictionary.Api.Abstracts;
using MyDictionary.Api.Contracts.DictionaryItems;
using MyDictionary.Api.Contracts.DictionaryItems.Requests;
using MyDictionary.Api.Contracts.DictionaryItems.Responses;
using MyDictionary.Application.Services.DictionaryItems.Commands;
using MyDictionary.Application.Services.DictionaryItems.Queries;

namespace MyDictionary.Api.Controllers;

[Authorize]
public class WordsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> List([FromBody] GetWordListRequest request)
    {
        var query = new GetWordListQuery(
            DictionaryId: request.DictionaryId,
            Term: request.Term,
            Meaning: request.Meaning,
            SortField: request.SortField,
            SortOrder: request.SortOrder,
            Page: request.Page,
            PageSize: request.PageSize
        );

        return await Send(query, WordListResponse.MapFrom);
    }

    [HttpPost]
    public async Task<IActionResult> Get([FromBody] GetWordRequest request)
    {
        var query = new GetWordQuery(
            Id: request.Id,
            IsIncludeExample: request.IsIncludeExample,
            IsIncludeWordForm: request.IsIncludeWordForm
        );
        return await Send(query, WordResponse.MapFrom);
    }

    [HttpGet]
    public async Task<IActionResult> Count([FromQuery] Guid dictionaryId)
    {
        var query = new GetWordCountQuery(DictionaryId: dictionaryId);
        return await Send(query);
    }

    public async Task<IActionResult> AggregateWeight([FromBody] GetWordWeightRequest request)
    {
        var query = new GetWordWeightQuery(
            DictionaryId: request.DictionaryId,
            WeightAggregate: request.WeightAggregate
        );
        return await Send(query);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateWordRequest request)
    {
        var command = new CreateWordCommand(
            DictionaryId: request.DictionaryId,
            Term: request.Term,
            Meaning: request.Meaning,
            Weight: request.Weight
        );

        return await Send(command);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateWordRequest request)
    {
        var command = new UpdateWordCommand(
            Id: request.Id,
            Term: request.Term,
            Meaning: request.Meaning,
            Weight: request.Weight
        );

        return await Send(command);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid Id)
    {
        var command = new DeleteWordCommand(Id: Id);
        return await Send(command);
    }
}
