using BlueHome.Server.API.Contracts.Requests;
using BlueHome.Server.Application.CommandHandlers;
using BlueHome.Server.Application.Commands;
using BlueHome.Server.Application.Devices.Commands;
using BlueHome.Server.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql.Internal.TypeHandlers.NumericHandlers;

namespace BlueHome.Server.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/devices")]
    public class DevicesController : ControllerBase
    {
        private readonly RegisterDeviceCommandHandler _handler;
        private readonly MoveDeviceHandler _moveHandler;

        public DevicesController(RegisterDeviceCommandHandler handler, MoveDeviceHandler moveHandler)
        {
            _handler = handler;
            _moveHandler = moveHandler;
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

        [HttpPost("move/space")]
        public async Task<IActionResult> MoveToSpace([FromBody] MoveToSpaceRequest request)
        {
            await _moveHandler.Handle(
                new MoveDeviceCommand(request.DeviceId, MoveTargetType.Space, request.SpaceId)
            );

            return Ok();
        }

        [HttpPost("move/location")]
        public async Task<IActionResult> MoveToLocation([FromBody] MoveToLocationRequest request)
        {
            await _moveHandler.Handle(
                new MoveDeviceCommand(request.DeviceId, MoveTargetType.Location, request.LocationId)
            );

            return Ok();
        }
    }
}
