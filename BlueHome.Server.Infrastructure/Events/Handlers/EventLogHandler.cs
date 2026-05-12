using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Application.Abstractions.Persistence;
using BlueHome.Server.Domain.Entities;
using BlueHome.Server.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.Events.Handlers
{
    public class EventLogHandler :
        IDomainEventHandler<DevicePoweredOnEvent>,
        IDomainEventHandler<DevicePoweredOffEvent>,
        IDomainEventHandler<DeviceBrightnessChangedEvent>
    {
        private readonly IBlueHomeDbContext _db;

        public EventLogHandler(IBlueHomeDbContext db)
        {
            _db = db;
        }

        public Task HandleAsync(DevicePoweredOnEvent e)
        {
            return Save(
                e.DeviceId,
                nameof(DevicePoweredOnEvent),
                "Device powered ON"
            );
        }

        public Task HandleAsync(DevicePoweredOffEvent e)
        {
            return Save(
                e.DeviceId,
                nameof(DevicePoweredOffEvent),
                "Device powered OFF"
            );
        }

        public Task HandleAsync(DeviceBrightnessChangedEvent e)
        {
            return Save(
                e.DeviceId,
                nameof(DeviceBrightnessChangedEvent),
                $"Brightness set to {e.Brightness}"
            );
        }

        private async Task Save(
            int deviceId,
            string eventType,
            string description)
        {

            var log = new EventLog
            {
                DeviceId = deviceId,
                EventType = eventType,
                Description = description,
                CreatedAt = DateTime.UtcNow
            };

            _db.EventLogs.Add(log);
            await _db.SaveChangesAsync();
        }
    }
}
