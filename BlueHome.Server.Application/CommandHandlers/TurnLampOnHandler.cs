using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.CommandHandlers
{
    public class TurnLampOnHandler
    {
        private readonly IDeviceRuntime _runtime;
        private readonly IEventPublisher _eventPublisher;

        public TurnLampOnHandler(
            IDeviceRuntime runtime,
            IEventPublisher eventPublisher)
        {
            _runtime = runtime;
            _eventPublisher = eventPublisher;
        }

        public void Handle(TurnLampOnCommand command)
        {
            var device = _runtime.GetDevice(command.DeviceId);

            device.PowerOn();

            _runtime.Save(device);

            _eventPublisher.Publish(device.DomainEvents);
            device.ClearDomainEvents();
        }
    }
}
