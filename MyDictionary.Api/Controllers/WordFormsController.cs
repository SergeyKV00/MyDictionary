using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDictionary.Api.Abstracts;
using MyDictionary.Api.Contracts.WordForms.Requests;
using MyDictionary.Api.Contracts.WordForms.Responses;
using MyDictionary.Application.Services.WordForms.Commands;
using MyDictionary.Application.Services.WordForms.Queries;

namespace MyDictionary.Api.Controllers;

[Authorize]
public class WordFormsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateWordFormRequest request)
    {
        var command = new CreateWordFormCommand(
            DictionaryItemId: request.DictionaryItemId,
            Infinitive: request.Infinitive,
            PastSimple: request.PastSimple,
            PastParticiple: request.PastParticiple
        );

        return await Send(command);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateWordFormRequest request)
    {
        var command = new UpdateWordFormCommand(
            Id: request.Id,
            Infinitive: request.Infinitive,
            PastSimple: request.PastSimple,
            PastParticiple: request.PastParticiple
        );

        return await Send(command);
    }

    [HttpPost]
    public async Task<IActionResult> Get([FromBody] GetWordFormRequest request)
    {
        var query = new GetWordFormQuery(
            Id: request.Id,
            DictionaryItemId: request.DictionaryItemId
        );

        return await Send(query, WordFormResponse.Map);
    }
}
