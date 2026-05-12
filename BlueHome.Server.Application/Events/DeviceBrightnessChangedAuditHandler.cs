using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Application.Abstractions.Persistence;
using BlueHome.Server.Domain.Entities;
using BlueHome.Server.Domain.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Events
{
    public class DeviceBrightnessChangedAuditHandler
        : IEventHandler<DeviceBrightnessChangedEvent>
    {
        private readonly IBlueHomeDbContext _db;

        public DeviceBrightnessChangedAuditHandler(IBlueHomeDbContext db)
        {
            _db = db;
        }

        public async Task Handle(DeviceBrightnessChangedEvent @event)
        {
            var device = await _db.Devices
                .AsNoTracking()
                .FirstAsync(x => x.DeviceId == @event.DeviceId);

            _db.UserLogs.Add(new UserLog
            {
                UserId = @event.UserId,
                SpaceId = device.SpaceId,
                Description =
                    $"Changed brightness of device #{device.DeviceId} to {@event.Brightness}",
                CreatedAt = DateTime.UtcNow
            });
        }
    }
}
