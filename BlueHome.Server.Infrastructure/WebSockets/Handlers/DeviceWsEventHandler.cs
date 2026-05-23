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
        IEventHandler<DevicePoweredOnEvent>,
        IEventHandler<DevicePoweredOffEvent>,
        IEventHandler<DeviceBrightnessChangedEvent>
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

        public Task Handle(DevicePoweredOnEvent e)
        {
            return _onPublisher.Publish(e);
        }

        public Task Handle(DevicePoweredOffEvent e)
        {
            return _offPublisher.Publish(e);
        }

        public Task Handle(DeviceBrightnessChangedEvent e)
        {
            return _brightnessPublisher.Publish(e);
        }
    }
}
