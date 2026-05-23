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
    public sealed class GetSpaceLocationsQueryHandler
    {
        private readonly IBlueHomeDbContext _db;

        public GetSpaceLocationsQueryHandler(IBlueHomeDbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<UserLocationDto>> Handle(
            GetSpaceLocationsQuery query,
            CancellationToken cancellationToken)
        {
            var hasAccess = await _db.UserLogs
                .AnyAsync(ul =>
                    ul.UserId == query.UserId &&
                    ul.SpaceId == query.SpaceId,
                    cancellationToken);

            if (!hasAccess)
                throw new UnauthorizedAccessException("No access to space");

            return await _db.Locations
                .Where(l => l.SpaceId == query.SpaceId)
                .Select(l => new UserLocationDto(
                    l.LocationId,
                    l.LocationName,
                    l.SpaceId
                ))
                .ToListAsync(cancellationToken);
        }
    }
}
