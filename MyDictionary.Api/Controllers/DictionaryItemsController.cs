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
public class DictionaryItemsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> List([FromBody] GetDictionaryItemListRequest request)
    {
        var query = new GetDictionaryItemQueryList(
            DictionaryId: request.DictionaryId,
            Term: request.Term,
            Meaning: request.Meaning,
            SortField: request.SortField,
            SortOrder: request.SortOrder,
            Page: request.Page,
            PageSize: request.PageSize,
            IsIncludeWordProgress: request.IsIncludeWordProgress
        );

        return await Send(query, DictionaryItemListResponse.MapFrom);
    }

    [HttpPost]
    public async Task<IActionResult> Get([FromBody] GetDictionaryItemRequest request)
    {
        var query = new GetDictionaryItemQuery(
            Id: request.Id,
            IsIncludeExample: request.IsIncludeExample,
            IsIncludeWordForm: request.IsIncludeWordForm,
            IsIncludeWordProgress: request.IsIncludeWordProgress
        );
        return await Send(query, DictionaryItemResponse.MapFrom);
    }

    [HttpGet]
    public async Task<IActionResult> Count([FromQuery] Guid dictionaryId)
    {
        var query = new GetDictionaryItemCountQuery(DictionaryId: dictionaryId);
        return await Send(query);
    }

    public async Task<IActionResult> AggregateWeight([FromBody] GetDictionaryItemWeightRequest request)
    {
        var query = new GetDictionaryItemWeightQuery(
            DictionaryId: request.DictionaryId,
            WeightAggregate: request.WeightAggregate
        );
        return await Send(query);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDictionaryItemRequest request)
    {
        var command = new CreateDictionaryItemCommand(
            DictionaryId: request.DictionaryId,
            Term: request.Term,
            Meaning: request.Meaning,
            Weight: request.Weight
        );

        return await Send(command);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateDictionaryItemRequest request)
    {
        var command = new UpdateDictionaryItemCommand(
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
        var command = new DeleteDictionaryItemCommand(Id: Id);
        return await Send(command);
    }
}
