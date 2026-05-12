using BlueHome.Server.API.Contracts.Requests;
using BlueHome.Server.Application.Locations.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlueHome.Server.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/locations")]
    public class LocationsController : ControllerBase
    {
        private readonly CreateLocationCommandHandler _handler;

        public LocationsController(CreateLocationCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateLocationRequest request,
            CancellationToken ct)
        {
            var result = await _handler.Handle(
                new CreateLocationCommand(request.Name),
                ct);

            return Ok(result);
        }
    }
}
