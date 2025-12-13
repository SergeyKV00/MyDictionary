using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDictionary.Api.Abstracts;
using MyDictionary.Api.Contracts.UserDictionaries;
using MyDictionary.Api.Contracts.UserDictionaries.Requests;
using MyDictionary.Api.Contracts.UserDictionaries.Responses;
using MyDictionary.Api.Contracts.UserDictionary;
using MyDictionary.Application.Services.UserDictionaries.Commands;
using MyDictionary.Application.Services.UserDictionaries.Queries;
using MyDictionary.Domain;

namespace MyDictionary.Api.Controllers;

[Authorize]
public class UserDictionariesController(SessionContext session) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> List(GetUserDictionaryListRequest request)
    {
        var query = new GetUserDictionaryListQuery(
            UserId: session.UserId,
            IsIncludeItems: request.IsIncludeItems,
            Ids: request.Ids
        );
        return await Send(query, UserDictionaryListResponse.From);
    }

    [HttpPost]
    public async Task<IActionResult> SearchAcross([FromBody] UserDictionarySearchAcrossResponse request)
    {
        var query = new SearchAcrosssUserDictionariesQueryList(Term: request.Term);
        return await Send(query);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Guid id)
    {
        var query = new GetUserDictionaryQuery(Id: id);
        return await Send(query, UserDictionaryResponse.MapFrom);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDictionaryRequest requests)
    {
        var command = new CreateUserDictionaryCommand(
            UserId: session.UserId,
            Name: requests.Name
        );

        return await Send(command);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        var command = new DeleteUserDictionaryCommand(id);
        return await Send(command);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatUserDictionaryRequest request)
    {
        var command = new UpdateUserDictionaryCommand(
            Id: request.Id,
            Name: request.Name,
            UserId: session.UserId
        );

        return await Send(command);
    }
}
