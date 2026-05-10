using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Domain.Events
{
    public sealed class LampPoweredOffEvent : IDomainEvent
    {
        public Guid LampId { get; }
        public DateTime OccurredAt { get; } = DateTime.UtcNow;

        public LampPoweredOffEvent(Guid lampId)
        {
            LampId = lampId;
        }
    }
}
