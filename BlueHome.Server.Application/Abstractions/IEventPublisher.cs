using BlueHome.Server.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Abstractions
{
    public interface IEventPublisher
    {
        void Publish(IEnumerable<IDomainEvent> events);
    }
}
