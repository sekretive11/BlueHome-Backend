using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Domain.Events
{
    public record DeviceBrightnessChangedEvent(int DeviceId, int Brightness, DateTime OccurredAt) : IDomainEvent;
}
