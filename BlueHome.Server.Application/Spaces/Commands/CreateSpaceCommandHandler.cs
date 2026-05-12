using BlueHome.Server.Application.Abstractions.Persistence;
using BlueHome.Server.Application.Spaces.Abstractions;
using BlueHome.Server.Application.Spaces.DTO;
using BlueHome.Server.Domain.Entities;
using BlueHome.Server.Domain.Enums;
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
        private readonly IBlueHomeDbContext _db;

        public CreateSpaceCommandHandler(ISpaceRepository spaceRepository, IBlueHomeDbContext db)
        {
            _spaceRepository = spaceRepository;
            _db = db;
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

            var userLog = new UserLog
            {
                UserId = command.UserId,
                SpaceId = space.SpaceId,
                Role = SpaceRole.Owner,
                Description = "Space created",
                CreatedAt = DateTime.UtcNow
            };

            _db.UserLogs.Add(userLog);
            await _db.SaveChangesAsync(cancellationToken);

            return new SpaceDto(
                space.SpaceId,
                space.SpaceName,
                space.SpaceType
            );
        }
    }
}
