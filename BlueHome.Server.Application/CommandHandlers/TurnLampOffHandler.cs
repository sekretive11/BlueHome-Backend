using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.CommandHandlers
{
    public class TurnLampOffHandler
    {
        private readonly IDeviceRuntime _runtime;
        private readonly IEventPublisher _eventPublisher;

        public TurnLampOffHandler(
            IDeviceRuntime runtime,
            IEventPublisher eventPublisher)
        {
            _runtime = runtime;
            _eventPublisher = eventPublisher;
        }

        public void Handle(TurnLampOffCommand command)
        {
            var device = _runtime.GetDevice(command.DeviceId);

            device.PowerOff();

            _runtime.Save(device);

            _eventPublisher.Publish(device.DomainEvents);
            device.ClearDomainEvents();
        }
    }
}
