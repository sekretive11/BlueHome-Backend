using BlueHome.Server.API.Contracts.Requests;
using BlueHome.Server.Application.Abstractions.Auth;
using BlueHome.Server.Application.Spaces.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlueHome.Server.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SpacesController : ControllerBase
    {
        private readonly CreateSpaceCommandHandler _handler;
        private readonly ICurrentUserService _currentUser;

        public SpacesController(CreateSpaceCommandHandler handler, ICurrentUserService currentUser)
        {
            _handler = handler;
            _currentUser = currentUser;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpace(
            [FromBody] CreateSpaceRequest request,
            CancellationToken cancellationToken)
        {

            var command = new CreateSpaceCommand(
                _currentUser.UserId,
                request.Name,
                request.Type
            );

            var result = await _handler.Handle(command, cancellationToken);

            return Created("", result);
        }
    }
}
