using BlueHome.Server.API.Contracts.Requests;
using BlueHome.Server.Application.Spaces.Commands;
using Microsoft.AspNetCore.Mvc;

namespace BlueHome.Server.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpacesController : ControllerBase
    {
        private readonly CreateSpaceCommandHandler _handler;

        public SpacesController(CreateSpaceCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpace(
            [FromBody] CreateSpaceRequest request,
            CancellationToken cancellationToken)
        {
            // временно: заглушка пользователя (позже JWT)
            var userId = Guid.NewGuid();

            var command = new CreateSpaceCommand(
                userId,
                request.Name,
                request.Type
            );

            var result = await _handler.Handle(command, cancellationToken);

            return Created("", result);
        }
    }
}
