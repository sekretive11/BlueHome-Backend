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
    public sealed class GetUserLocationsQueryHandler
    {
        private readonly IBlueHomeDbContext _db;

        public GetUserLocationsQueryHandler(IBlueHomeDbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<UserLocationDto>> Handle(
            GetUserLocationsQuery query,
            CancellationToken cancellationToken)
        {
            return await _db.Locations
                .Where(l =>
                    _db.UserLogs.Any(ul =>
                        ul.UserId == query.UserId &&
                        ul.SpaceId == l.SpaceId))
                .Select(l => new UserLocationDto(
                    l.LocationId,
                    l.LocationName,
                    l.SpaceId
                ))
                .ToListAsync(cancellationToken);
        }
    }
}
