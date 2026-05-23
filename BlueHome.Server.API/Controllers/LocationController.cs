using BlueHome.Server.API.Contracts.Requests;
using BlueHome.Server.Application.Locations.Commands;
using BlueHome.Server.Application.Abstractions.Auth;
using BlueHome.Server.Application.Locations.Handlers;
using BlueHome.Server.Application.Locations.Queries;
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
        private readonly GetUserLocationsQueryHandler _getUserLocationsHandler;
        private readonly GetSpaceLocationsQueryHandler _getSpaceLocationsHandler;
        private readonly GetLocationByIdQueryHandler _getLocationByIdHandler;
        private readonly ICurrentUserService _currentUser;

        public LocationsController(
            CreateLocationCommandHandler handler,
            GetUserLocationsQueryHandler getUserLocationsHandler,
            GetSpaceLocationsQueryHandler getSpaceLocationsHandler,
            GetLocationByIdQueryHandler getLocationByIdHandler,
            ICurrentUserService currentUser)
        {
            _handler = handler;
            _getUserLocationsHandler = getUserLocationsHandler;
            _getSpaceLocationsHandler = getSpaceLocationsHandler;
            _getLocationByIdHandler = getLocationByIdHandler;
            _currentUser = currentUser;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateLocationRequest request,
            CancellationToken ct)
        {
            var result = await _handler.Handle(
                new CreateLocationCommand(request.Name, request.SpaceId),
                ct);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserLocations(CancellationToken ct)
        {
            var result = await _getUserLocationsHandler.Handle(
                new GetUserLocationsQuery(_currentUser.UserId),
                ct);

            return Ok(result);
        }

        [HttpGet("space/{spaceId:int}")]
        public async Task<IActionResult> GetLocationsBySpace(int spaceId, CancellationToken ct)
        {
            var result = await _getSpaceLocationsHandler.Handle(
                new GetSpaceLocationsQuery(spaceId, _currentUser.UserId),
                ct);

            return Ok(result);
        }

        [HttpGet("{locationId:int}")]
        public async Task<IActionResult> GetById(int locationId, CancellationToken ct)
        {
            var result = await _getLocationByIdHandler.Handle(
                new GetLocationByIdQuery(locationId, _currentUser.UserId),
                ct);

            return Ok(result);
        }
    }
}
