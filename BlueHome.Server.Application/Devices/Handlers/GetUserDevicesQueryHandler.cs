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
    public sealed class GetUserDevicesQueryHandler
    {
        private readonly IBlueHomeDbContext _db;

        public GetUserDevicesQueryHandler(IBlueHomeDbContext db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<UserDeviceDto>> Handle(
            GetUserDevicesQuery query,
            CancellationToken cancellationToken)
        {
            return await _db.Devices
                .Where(d =>
                    _db.UserLogs.Any(ul =>
                        ul.UserId == query.UserId &&
                        ul.SpaceId == d.SpaceId))
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
