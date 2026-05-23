using BlueHome.Server.API.Contracts.Requests;
using BlueHome.Server.Application.CommandHandlers;
using BlueHome.Server.Application.Commands;
using BlueHome.Server.Application.Devices.Commands;
using BlueHome.Server.Domain.Enums;
using BlueHome.Server.Application.Abstractions.Auth;
using BlueHome.Server.Application.Devices.Handlers;
using BlueHome.Server.Application.Devices.Queries;
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
        private readonly GetUserDevicesQueryHandler _getUserDevicesHandler;
        private readonly GetLocationDevicesQueryHandler _getLocationDevicesHandler;
        private readonly GetDeviceByIdQueryHandler _getDeviceByIdHandler;
        private readonly ICurrentUserService _currentUser;

        public DevicesController(
            RegisterDeviceCommandHandler handler,
            MoveDeviceHandler moveHandler,
            GetUserDevicesQueryHandler getUserDevicesHandler,
            GetLocationDevicesQueryHandler getLocationDevicesHandler,
            GetDeviceByIdQueryHandler getDeviceByIdHandler,
            ICurrentUserService currentUser)
        {
            _handler = handler;
            _moveHandler = moveHandler;
            _getUserDevicesHandler = getUserDevicesHandler;
            _getLocationDevicesHandler = getLocationDevicesHandler;
            _getDeviceByIdHandler = getDeviceByIdHandler;
            _currentUser = currentUser;
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

        [HttpGet("location/{locationId:int}")]
        public async Task<IActionResult> GetDevicesByLocation(int locationId, CancellationToken ct)
        {
            var result = await _getLocationDevicesHandler.Handle(
                new GetLocationDevicesQuery(locationId, _currentUser.UserId),
                ct);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserDevices(CancellationToken ct)
        {
            var result = await _getUserDevicesHandler.Handle(
                new GetUserDevicesQuery(_currentUser.UserId),
                ct);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var result = await _getDeviceByIdHandler.Handle(
                new GetDeviceByIdQuery(id, _currentUser.UserId),
                ct);

            return Ok(result);
        }
    }
}
