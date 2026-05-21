using BlueHome.Server.Application.Abstractions.Persistence;
using BlueHome.Server.Application.Locations.DTO;
using BlueHome.Server.Application.Locations.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Locations.Handlers
{
    public sealed class GetLocationByIdQueryHandler
    {
        private readonly IBlueHomeDbContext _db;

        public GetLocationByIdQueryHandler(IBlueHomeDbContext db)
        {
            _db = db;
        }

        public async Task<UserLocationDto> Handle(GetLocationByIdQuery query, CancellationToken ct)
        {
            var hasAccess = await _db.Locations
                .AnyAsync(l =>
                    l.LocationId == query.LocationId &&
                    _db.Devices.Any(d =>
                        d.LocationId == l.LocationId &&
                        _db.UserLogs.Any(ul =>
                            ul.UserId == query.UserId &&
                            ul.SpaceId == d.SpaceId)), ct);

            if (!hasAccess)
                throw new Exception("Location not found or access denied");

            var location = await _db.Locations
                .Where(l => l.LocationId == query.LocationId)
                .Select(l => new UserLocationDto(
                    l.LocationId,
                    l.LocationName,
                    l.SpaceId
                ))
                .FirstOrDefaultAsync(ct);

            return location!;
        }
    }
}
