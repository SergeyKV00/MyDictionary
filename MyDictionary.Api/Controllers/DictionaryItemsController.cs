using Microsoft.AspNetCore.Mvc;
using MyDictionary.Api.Abstracts;
using MyDictionary.Api.Contracts.DictionaryItems;
using MyDictionary.Api.Contracts.DictionaryItems.Models;
using MyDictionary.Api.Extensions;
using MyDictionary.Application.Services.DictionaryItems.Commands;
using MyDictionary.Application.Services.DictionaryItems.Queries;

namespace MyDictionary.Api.Controllers;

public class DictionaryItemsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> List([FromBody] GetDictionaryItemListRequest request)
    {
        var query = new GetDictionaryItemQueryList(
            DictionaryId: request.DictionaryId,
            Term: request.Term,
            Meaning: request.Meaning
        );

        var result = await Mediator.Send(query);
        return HandleResult(result.MapList(r => r.ToResponse()));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDictionaryItemRequest request)
    {
        var command = new CreateDictionaryItemCommand(
            DictionaryId: request.DictionaryId,
            Term: request.Term,
            Meaning: request.Meaning
        );

        return HandleResult(await Mediator.Send(command));
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

        return HandleResult(await Mediator.Send(command));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid Id)
    {
        var command = new DeleteDictionaryItemCommand(Id: Id);
        return HandleResult(await Mediator.Send(command));
    }
}
