using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDictionary.Api.Abstracts;
using MyDictionary.Api.Contracts.DictionaryItemExample.Requests;
using MyDictionary.Api.Contracts.DictionaryItemExample.Responses;
using MyDictionary.Application.Services.DictionaryItemExamples.Commands;
using MyDictionary.Application.Services.DictionaryItemExamples.Queries;

namespace MyDictionary.Api.Controllers;

[Authorize]
public class DictionaryItemExamplesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] 
        CreateDictionaryItemExampleRequest request
    )
    {
        var command = new CreateDictionaryItemExampleCommand(
            DictionaryItemId: request.DictionaryItemId,
            Example: request.Example,
            Translation: request.Translation
        );

        return await Send(command);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody]
        UpdateDictionaryItemExampleRequest request
    )
    {
        var command = new UpdateDictionaryItemExampleCommand(
            Id: request.Id,
            Example: request.Example,
            Translation: request.Translation
        );

        return await Send(command);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid Id)
    {
        var command = new DeleteDictionaryItemExampleCommand(Id);
        return await Send(command);
    }

    [HttpPost]
    public async Task<IActionResult> List([FromBody] 
        GetDictionaryItemExampleListRequest request
    )
    {
        var query = new GetDictionaryItemExampleQueryList(
            DicitonaryItemId: request.DictionaryItemId
        );

        return await Send(query, DictionaryItemExampleListResponse.MapFrom);
    }
}
