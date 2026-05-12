using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Domain.Events
{
    public sealed class DeviceMovedEvent : IDomainEvent
    {
        public int DeviceId { get; }
        public string TargetType { get; }
        public int UserId { get; }
        public int TargetId { get; }
        public DateTime OccurredAt { get; }

        public DeviceMovedEvent(
            int deviceId,
            string targetType,
            int targetId,
            int userId)
        {
            DeviceId = deviceId;
            TargetType = targetType;
            TargetId = targetId;
            UserId = userId;
            OccurredAt = DateTime.UtcNow;
        }
    }
}
