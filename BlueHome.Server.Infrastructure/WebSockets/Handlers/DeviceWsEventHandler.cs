using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.WebSockets.Handlers
{
    public class DeviceWsEventHandler :
        IDomainEventHandler<DevicePoweredOnEvent>,
        IDomainEventHandler<DevicePoweredOffEvent>,
        IDomainEventHandler<DeviceBrightnessChangedEvent>
    {
        private readonly IDeviceEventWsPublisher<DevicePoweredOnEvent> _onPublisher;
        private readonly IDeviceEventWsPublisher<DevicePoweredOffEvent> _offPublisher;
        private readonly IDeviceEventWsPublisher<DeviceBrightnessChangedEvent> _brightnessPublisher;

        public DeviceWsEventHandler(
            IDeviceEventWsPublisher<DevicePoweredOnEvent> onPublisher,
            IDeviceEventWsPublisher<DevicePoweredOffEvent> offPublisher,
            IDeviceEventWsPublisher<DeviceBrightnessChangedEvent> brightnessPublisher)
        {
            _onPublisher = onPublisher;
            _offPublisher = offPublisher;
            _brightnessPublisher = brightnessPublisher;
        }

        public Task HandleAsync(DevicePoweredOnEvent e)
        {
            Console.WriteLine("WS HANDLER → DevicePoweredOnEvent");
            return _onPublisher.Publish(e);
        }

        public Task HandleAsync(DevicePoweredOffEvent e)
        {
            return _offPublisher.Publish(e);
        }

        public Task HandleAsync(DeviceBrightnessChangedEvent e)
        {
            return _brightnessPublisher.Publish(e);
        }
    }
}
