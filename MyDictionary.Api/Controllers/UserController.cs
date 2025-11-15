using Microsoft.AspNetCore.Mvc;
using MyDictionary.Api.Abstracts;
using MyDictionary.Api.Contracts.User;
using MyDictionary.Application.Services.Users.Commands;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
        {
            var command = new CreateUserCommand(
                UserName: request.UserName,
                Email: request.Email,
                Password: request.Password
            );

            return HandleResult(await Mediator.Send(command));
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var command = new LoginUserCommand(
                UserName: request.UserName,
                Password: request.Password
            );

            return HandleResult(await Mediator.Send(command));
        }
    }
}
