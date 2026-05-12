using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Application.Abstractions.Persistence;
using BlueHome.Server.Domain.Entities;
using BlueHome.Server.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlueHome.Server.Application.Events
{
    public class DeviceMovedAuditHandler : IEventHandler<DeviceMovedEvent>
    {
        private readonly IBlueHomeDbContext _db;

        public DeviceMovedAuditHandler(IBlueHomeDbContext db)
        {
            _db = db;
        }

        public async Task Handle(DeviceMovedEvent @event)
        {
            var device = await _db.Devices
                .AsNoTracking()
                .FirstAsync(x => x.DeviceId == @event.DeviceId);

            _db.UserLogs.Add(new UserLog
            {
                UserId = @event.UserId,
                SpaceId = device.SpaceId,
                Description =
                    $"Moved device #{device.DeviceId} to {@event.TargetType} {@event.TargetId}",
                CreatedAt = DateTime.UtcNow
            });
        }
    }
}
