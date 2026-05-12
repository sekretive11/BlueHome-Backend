using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Domain.Events
{
    public sealed class DevicePoweredOffEvent : IDomainEvent
    {
        public int DeviceId { get; }
        public int UserId { get; }
        public DateTime OccurredAt { get; }

        public DevicePoweredOffEvent(int deviceId, int userId)
        {
            DeviceId = deviceId;
            UserId = userId;
            OccurredAt = DateTime.UtcNow;
        }
    }
}
