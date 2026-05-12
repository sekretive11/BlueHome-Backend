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
    public class DevicePoweredOnAuditHandler : IEventHandler<DevicePoweredOnEvent>
    {
        private readonly IBlueHomeDbContext _db;

        public DevicePoweredOnAuditHandler(IBlueHomeDbContext db)
        {
            _db = db;
        }

        public async Task Handle(DevicePoweredOnEvent @event)
        {
            var device = await _db.Devices
                .AsNoTracking()
                .FirstAsync(x => x.DeviceId == @event.DeviceId);

            _db.UserLogs.Add(new UserLog
            {
                UserId = @event.UserId,
                SpaceId = device.SpaceId,
                Description = $"Turned ON device #{device.DeviceId}",
                CreatedAt = DateTime.UtcNow
            });
        }
    }
}
