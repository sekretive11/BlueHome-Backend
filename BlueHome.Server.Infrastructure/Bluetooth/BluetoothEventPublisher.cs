using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.Bluetooth
{
    public class BluetoothEventPublisher : IEventPublisher
    {
        private readonly IBluetoothGateway _bluetoothGateway;

        public BluetoothEventPublisher(IBluetoothGateway bluetoothGateway)
        {
            _bluetoothGateway = bluetoothGateway;
        }

        public void Publish(IEnumerable<IDomainEvent> events)
        {
            foreach (var domainEvent in events)
            {
                _bluetoothGateway.Send(domainEvent);
            }
        }
    }
}
