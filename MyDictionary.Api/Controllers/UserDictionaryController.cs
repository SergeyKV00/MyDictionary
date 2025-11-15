using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDictionary.Api.Abstracts;
using MyDictionary.Api.Contracts.UserDictionary;
using MyDictionary.Application.Services.UserDictionaries.Commands;
using MyDictionary.Application.Services.UserDictionaries.Queries;
using MyDictionary.Domain;

namespace MyDictionary.Api.Controllers;

[Authorize]
public class UserDictionaryController(SessionContext session) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> List()
    {
        var query = new GetUserDictionaryListQuery(UserId: session.UserId);
        return HandleResult(await Mediator.Send(query));
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetUserDictionaryQueryOld query)
    {
        var model = await Mediator.Send(query);
        return Ok(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserDictionaryCreateRequest requests)
    {
        var command = new CreateUserDictionaryCommand(
            UserId: session.UserId,
            Name: requests.Name
        );

        return HandleResult(await Mediator.Send(command));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        var command = new DeleteUserDictionaryCommand(id);
        return HandleResult(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateDictionaryRequest request)
    {
        var command = new UpdateUserDictionaryCommand(
            Id: request.Id,
            Name: request.Name,
            UserId: session.UserId
        );

        return HandleResult(await Mediator.Send(command));
    }
}
