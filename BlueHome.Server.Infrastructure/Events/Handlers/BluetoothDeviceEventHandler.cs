using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.Events.Handlers
{
    public class BluetoothDeviceEventHandler :
        IDomainEventHandler<DevicePoweredOnEvent>,
        IDomainEventHandler<DevicePoweredOffEvent>,
        IDomainEventHandler<DeviceBrightnessChangedEvent>
    {
        private readonly IBluetoothGateway _bluetooth;

        public BluetoothDeviceEventHandler(IBluetoothGateway bluetooth)
        {
            _bluetooth = bluetooth;
        }

        public Task HandleAsync(DevicePoweredOnEvent e)
        {
            _bluetooth.Send(e);
            return Task.CompletedTask;
        }

        public Task HandleAsync(DevicePoweredOffEvent e)
        {
            _bluetooth.Send(e);
            return Task.CompletedTask;
        }

        public Task HandleAsync(DeviceBrightnessChangedEvent e)
        {
            _bluetooth.Send(e);
            return Task.CompletedTask;
        }
    }
}
