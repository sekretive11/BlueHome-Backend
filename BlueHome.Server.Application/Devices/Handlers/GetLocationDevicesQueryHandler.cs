using BlueHome.Server.Application.Abstractions.Persistence;
using BlueHome.Server.Application.Devices.DTO;
using BlueHome.Server.Application.Devices.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Devices.Handlers
{
    public sealed class GetLocationDevicesQueryHandler
    {
        private readonly IBlueHomeDbContext _db;

        public GetLocationDevicesQueryHandler(IBlueHomeDbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<UserDeviceDto>> Handle(
            GetLocationDevicesQuery query,
            CancellationToken cancellationToken)
        {
            var hasAccess = await _db.UserLogs
                .AnyAsync(ul =>
                    ul.UserId == query.UserId &&
                    _db.Locations.Any(l =>
                        l.LocationId == query.LocationId &&
                        l.SpaceId == ul.SpaceId),
                    cancellationToken);

            if (!hasAccess)
                throw new UnauthorizedAccessException("No access to location");

            return await _db.Devices
                .Where(d => d.LocationId == query.LocationId)
                .Select(d => new UserDeviceDto(
                    d.DeviceId,
                    d.SpaceId,
                    d.LocationId,
                    d.DeviceName,
                    d.Status.ToString(),
                    d.DeviceType.ToString()
                ))
                .ToListAsync(cancellationToken);
        }
    }
}
