using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Application.Abstractions.Persistence;
using BlueHome.Server.Application.Devices.DTO;
using BlueHome.Server.Application.Devices.Queries;
using BlueHome.Server.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Devices.Handlers
{
    public sealed class GetDeviceByIdQueryHandler
    {
        private readonly IBlueHomeDbContext _db;
        private readonly IDeviceRuntime _runtime;

        public GetDeviceByIdQueryHandler(
            IBlueHomeDbContext db,
            IDeviceRuntime runtime)
        {
            _db = db;
            _runtime = runtime;
        }

        public async Task<DeviceDetailsDto> Handle(GetDeviceByIdQuery query, CancellationToken ct)
        {
            var device = await _db.Devices
                .Where(d => d.DeviceId == query.DeviceId)
                .Join(_db.Spaces, d => d.SpaceId, s => s.SpaceId, (d, s) => new { d, s })
                .Join(_db.UserLogs, ds => ds.s.SpaceId, ul => ul.SpaceId, (ds, ul) => new { ds.d, ul })
                .Where(x => x.ul.UserId == query.UserId)
                .Select(x => x.d)
                .FirstOrDefaultAsync(ct);

            if (device == null)
                throw new Exception("Device not found or access denied");

            bool? isOn = null;
            int? brightness = null;

            if (device.DeviceType == DeviceType.Lamp)
            {
                var session = _runtime.GetSession(device.DeviceId);

                if (session is not null)
                {
                    isOn = session.Device.Status == DeviceStatus.online;
                    brightness = session.Device.Brightness?.Value;
                }
                else
                {
                    isOn = device.Status == DeviceStatus.online;
                    brightness = 50;
                }
            }

            return new DeviceDetailsDto(
                device.DeviceId,
                device.SpaceId,
                device.LocationId,
                device.DeviceName,
                device.DeviceType.ToDisplayName(),
                device.Status.ToString(),
                isOn,
                brightness
            );
        }
    }
}
