using BlueHome.Server.Application.Abstractions.Auth;
using BlueHome.Server.Application.Users.Handlers;
using BlueHome.Server.Application.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlueHome.Server.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly GetUserByIdQueryHandler _handler;
        private readonly ICurrentUserService _currentUser;

        public UsersController(GetUserByIdQueryHandler handler, ICurrentUserService currentUser)
        {
            _handler = handler;
            _currentUser = currentUser;
        }

        [Authorize(Roles = "Администратор")]
        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetById(
            int userId,
            CancellationToken ct)
        {
            var result = await _handler.Handle(
                new GetUserByIdQuery(userId),
                ct);

            return Ok(result);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMe(CancellationToken ct)
        {
            var result = await _handler.Handle(
                new GetUserByIdQuery(_currentUser.UserId),
                ct);

            return Ok(result);
        }
    }
}
