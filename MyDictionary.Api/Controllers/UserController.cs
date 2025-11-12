using Microsoft.AspNetCore.Mvc;
using MyDictionary.Api.Abstracts;
using MyDictionary.Application.Services.Users.Queries;

namespace MyDictionary.Api.Controllers
{
    public class UserController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var query = new GetUserQuery(id);
            return HandleResult(await Mediator.Send(query));
        }
    }
}
