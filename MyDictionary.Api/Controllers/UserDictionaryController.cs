using Microsoft.AspNetCore.Mvc;
using MyDictionary.Application.UserDictionaries.Commands;
using MyDictionary.Application.UserDictionaries.Queries;

namespace MyDictionary.Api.Controllers
{
    public class UserDictionaryController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult> List([FromBody] GetUserDictionaryListQuery query)
        {
            var list = await Mediator.Send(query);
            return Ok(list);
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] GetUserDictionaryQuery query)
        {
            var model = await Mediator.Send(query);
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateUserDictionaryCommand command)
        {
            //TODO Need Session
            command.UserId = Guid.NewGuid();

            await Mediator.Send(command);
            return Created();
        }
    }
}
