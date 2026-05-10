using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Application.Commands;
using BlueHome.Server.Domain.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.CommandHandlers
{
    public class SetLampBrightnessHandler
    {
        private readonly IDeviceRuntime _runtime;
        private readonly IEventPublisher _eventPublisher;

        public SetLampBrightnessHandler(
            IDeviceRuntime runtime,
            IEventPublisher eventPublisher)
        {
            _runtime = runtime;
            _eventPublisher = eventPublisher;
        }

        public void Handle(SetLampBrightnessCommand command)
        {
            var device = _runtime.GetDevice(command.DeviceId);

            device.SetBrightness(command.Brightness);

            _runtime.Save(device);

            _eventPublisher.Publish(device.DomainEvents);
            device.ClearDomainEvents();
        }
    }
}
