using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDictionary.Api.Abstracts;
using MyDictionary.Api.Contracts.StudyDecks.Requests;
using MyDictionary.Api.Contracts.StudyDecks.Responses;
using MyDictionary.Application.Services.StudyDecks.Commands;
using MyDictionary.Application.Services.StudyDecks.Queries;
using MyDictionary.Domain;

namespace MyDictionary.Api.Controllers;

[Authorize]
public class StudyDecksController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStudyDeckRequest request)
    {
        var command = new CreateStudyDeckCommand(
            Name: request.Name,
            Description: request.Description
        );

        return await Send(command);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        var command = new DeleteStudyDeckCommand(id);
        return await Send(command);
    }

    [HttpPost]
    public async Task<IActionResult> List([FromBody] GetStudyDeckListRequest request)
    {
        var query = new GetStudyDeckListQuery();
        return await Send(query, StudyDeckResponse.MapFrom);
    }

    [HttpPost]
    public async Task<IActionResult> AddDictionary([FromBody] AddDictionaryToStudyDeckRequest request)
    {
        var command = new AddDictionaryToStudyDeckCommand(
            request.StudyDeckId, 
            request.DictionaryId, 
            request.IsSynchronized
        );
        return await Send(command);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateDictionary([FromBody] UpdateStudyDeckDictionaryRequest request)
    {
        var command = new UpdateStudyDeckDictionaryCommand(
            request.StudyDeckId,
            request.DictionaryId,
            request.IsSynchronized
        );
        return await Send(command);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveDictionary([FromBody] RemoveDictionaryFromStudyDeckRequest request)
    {
        var command = new RemoveDictionaryFromStudyDeckCommand(
            request.StudyDeckId,
            request.DictionaryId
        );
        return await Send(command);
    }

    [HttpPost]
    public async Task<IActionResult> AddWords([FromBody] AddWordsToStudyDeckRequest request)
    {
        var command = new AddWordsToStudyDeckCommand(
            request.StudyDeckId, 
            request.WordIds
        );
        return await Send(command);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveWords([FromBody] RemoveWordsFromStudyDeckRequest request)
    {
        var command = new RemoveWordsFromStudyDeckCommand(
            request.StudyDeckId,
            request.WordIds
        );
        return await Send(command);
    }

    [HttpPost]
    public async Task<IActionResult> Get([FromBody] GetStudyDeckRequest request)
    {
        var query = new GetStudyDeckQuery(request.Id);
        return await Send(query, deck => new StudyDeckDetailResponse(
            deck.Id,
            deck.Name,
            deck.Description,
            deck.Dictionaries
                .Where(x => x.Deleted == null)
                .Select(d => new StudyDeckDictionaryResponse(
                    d.DictionaryId,
                    d.Dictionary?.Name ?? "",
                    d.IsSynchronized
                )).ToList()
        ));
    }

    [HttpPost]
    public async Task<IActionResult> GetWords([FromBody] GetStudyDeckWordsRequest request)
    {
        var query = new GetStudyDeckWordsQuery(
            request.StudyDeckId, 
            request.Page, 
            request.PageSize,
            request.SortField,
            request.SortOrder
        );
        
        return await Send(query, word => new StudyDeckWordResponse(
            Id: word.DictionaryItemId,
            Term: word.DictionaryItem.Term,
            Meaning: word.DictionaryItem.Meaning
        ));
    }
}
