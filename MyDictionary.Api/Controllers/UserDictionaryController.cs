using Microsoft.AspNetCore.Mvc;
using MyDictionary.Api.Abstracts;
using MyDictionary.Api.Contracts.UserDictionary;
using MyDictionary.Application.Services.UserDictionaries.Commands;
using MyDictionary.Application.Services.UserDictionaries.Queries;

namespace MyDictionary.Api.Controllers;

public class UserDictionaryController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> List([FromBody] GetUserDictionaryListQuery query)
    {
        var list = await Mediator.Send(query);
        return Ok(list);
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
        //TODO Need Session
        var command = new CreateUserDictionaryCommand(
            UserId: new Guid("3426FC5B-1BBF-F011-AD82-AC5AFCC04D1B"),
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
        //TODO Need Session
        var tsun = new Guid("3426FC5B-1BBF-F011-AD82-AC5AFCC04D1B");
        var command = new UpdateUserDictionaryCommand(
            Id: request.Id,
            Name: request.Name,
            UserId: tsun
        );

        return HandleResult(await Mediator.Send(command));
    }
}
