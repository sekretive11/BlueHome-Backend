using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Application.Abstractions.Auth;
using BlueHome.Server.Application.Abstractions.WebSockets;
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
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly ICurrentUserService _currentUser;
        private readonly IDeviceNotifier _notifier;

        public TurnLampOnHandler(
            IDeviceRuntime runtime,
            IDomainEventDispatcher dispatcher,
            ICurrentUserService currentUser,
            IDeviceNotifier notifier)
        {
            _runtime = runtime;
            _dispatcher = dispatcher;
            _currentUser = currentUser;
            _notifier = notifier;
        }

        public async Task Handle(TurnLampOnCommand command)
        {
            var device = _runtime.GetDevice(command.DeviceId);

            device.PowerOn(_currentUser.UserId);

            await _runtime.Save(device);

            await _dispatcher.DispatchAsync(device.DomainEvents);
            device.ClearDomainEvents();

            await _notifier.SendLampOn(command.DeviceId);
        }
    }
}
