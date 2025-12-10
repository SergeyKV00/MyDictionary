using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDictionary.Api.Abstracts;
using MyDictionary.Api.Contracts.WordProgresses.Requests;
using MyDictionary.Application.Services.WordProgresses.Commands;

namespace MyDictionary.Api.Controllers;

[Authorize]
public class WordProgressesController : BaseController
{
    [HttpPost] // TODO_S Replace Request
    public async Task<IActionResult> Create(CreateWordProgressCommand command)
    {
        return await Send(command);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitWordRecall(SubmitWordRecallRequest request)
    {
        var command = new SubmitWordRecallCommand(
            DictionaryItemId: request.DictiomaryItemId,
            Rating: request.Rating
        );

        return await Send(command);
    }
}
