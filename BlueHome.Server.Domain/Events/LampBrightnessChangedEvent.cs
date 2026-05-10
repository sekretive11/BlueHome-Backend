using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Domain.Events
{
    public sealed class LampBrightnessChangedEvent : IDomainEvent
    {
        public Guid LampId { get; }
        public int Brightness { get; }
        public DateTime OccurredAt { get; } = DateTime.UtcNow;

        public LampBrightnessChangedEvent(Guid lampId, int brightness)
        {
            LampId = lampId;
            Brightness = brightness;
        }
    }
}
