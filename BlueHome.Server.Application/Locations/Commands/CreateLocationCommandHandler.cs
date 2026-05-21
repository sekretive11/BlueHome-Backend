using BlueHome.Server.Application.Abstractions.Persistence;
using BlueHome.Server.Application.Locations.DTO;
using BlueHome.Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Locations.Commands
{
    public sealed class CreateLocationCommandHandler
    {
        private readonly IBlueHomeDbContext _db;

        public CreateLocationCommandHandler(IBlueHomeDbContext db)
        {
            _db = db;
        }

        public async Task<LocationDto> Handle(
            CreateLocationCommand command,
            CancellationToken ct)
        {
            var location = Location.Create(command.Name, command.SpaceId);

            _db.Locations.Add(location);
            await _db.SaveChangesAsync(ct);

            return new LocationDto(
                location.LocationId,
                location.LocationName
            );
        }
    }
}
