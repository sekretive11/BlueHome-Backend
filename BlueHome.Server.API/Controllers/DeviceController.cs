using BlueHome.Server.API.Contracts.Requests;
using BlueHome.Server.Application.Devices.Commands;
using Microsoft.AspNetCore.Mvc;

namespace BlueHome.Server.API.Controllers
{
    [ApiController]
    [Route("api/devices")]
    public class DevicesController : ControllerBase
    {
        private readonly RegisterDeviceCommandHandler _handler;

        public DevicesController(RegisterDeviceCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterDeviceRequest request,
            CancellationToken ct)
        {
            var result = await _handler.Handle(
                new RegisterDeviceCommand(
                    request.SpaceId,
                    request.LocationId,
                    request.Name,
                    request.Type
                ),
                ct);

            return Ok(result);
        }
    }
}
