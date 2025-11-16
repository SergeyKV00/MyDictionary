using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDictionary.Api.Abstracts;
using MyDictionary.Api.Contracts.User;
using MyDictionary.Api.Contracts.Users;
using MyDictionary.Application.Services.Users.Commands;
using MyDictionary.Application.Services.Users.Queries;

namespace MyDictionary.Api.Controllers
{
    public class UsersController : BaseController
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var query = new GetUserQuery(id);
            return await Send(query, UserResponse.MapFrom);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            var command = new CreateUserCommand(
                UserName: request.UserName,
                Email: request.Email,
                Password: request.Password
            );

            return await Send(command);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var command = new LoginUserCommand(
                UserName: request.UserName,
                Password: request.Password
            );

            return await Send(command);
        }
    }
}
