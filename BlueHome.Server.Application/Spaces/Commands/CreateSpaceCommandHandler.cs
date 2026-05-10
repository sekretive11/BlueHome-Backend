using BlueHome.Server.Application.Spaces.Abstractions;
using BlueHome.Server.Application.Spaces.DTO;
using BlueHome.Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Spaces.Commands
{
    public sealed class CreateSpaceCommandHandler
    {
        private readonly ISpaceRepository _spaceRepository;

        public CreateSpaceCommandHandler(ISpaceRepository spaceRepository)
        {
            _spaceRepository = spaceRepository;
        }

        public async Task<SpaceDto> Handle(
            CreateSpaceCommand command,
            CancellationToken cancellationToken)
        {
            var space = IoTSpace.Create(
                command.Name,
                command.Type
            );

            await _spaceRepository.AddAsync(space, cancellationToken);

            return new SpaceDto(
                space.SpaceId,
                space.SpaceName,
                space.SpaceType
            );
        }
    }
}
