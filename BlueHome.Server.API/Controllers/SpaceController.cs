using BlueHome.Server.API.Contracts.Requests;
using BlueHome.Server.Application.Abstractions.Auth;
using BlueHome.Server.Application.Spaces.Commands;
using BlueHome.Server.Application.Spaces.Handlers;
using BlueHome.Server.Application.Spaces.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlueHome.Server.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SpacesController : ControllerBase
    {
        private readonly CreateSpaceCommandHandler _createHandler;
        private readonly GetUserSpacesQueryHandler _getHandler;
        private readonly GetSpaceByIdQueryHandler _getSpaceByIdHandler;
        private readonly ICurrentUserService _currentUser;

        public SpacesController(
            CreateSpaceCommandHandler createHandler,
            GetUserSpacesQueryHandler getHandler,
            GetSpaceByIdQueryHandler getSpaceByIdHandler,
            ICurrentUserService currentUser)
        {
            _createHandler = createHandler;
            _getHandler = getHandler;
            _getSpaceByIdHandler = getSpaceByIdHandler;
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

            var result = await _createHandler.Handle(command, cancellationToken);

            return Created(string.Empty, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetSpacesByUser(CancellationToken cancellationToken)
        {
            var query = new GetUserSpacesQuery(_currentUser.UserId);

            var result = await _getHandler.Handle(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{spaceId:int}")]
        public async Task<IActionResult> GetById(int spaceId, CancellationToken ct)
        {
            var result = await _getSpaceByIdHandler.Handle(
                new GetSpaceByIdQuery(spaceId, _currentUser.UserId),
                ct);

            if (result == null)
                return Forbid();

            return Ok(result);
        }
    }
}
