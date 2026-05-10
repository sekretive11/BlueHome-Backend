using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Domain.Events
{
    public sealed class LampPoweredOnEvent : IDomainEvent
    {
        public Guid LampId { get; }
        public DateTime OccurredAt { get; } = DateTime.UtcNow;

        public LampPoweredOnEvent(Guid lampId)
        {
            LampId = lampId;
        }
    }
}
