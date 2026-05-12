using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Application.Abstractions.Auth;
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
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly ICurrentUserService _currentUser;

        public TurnLampOffHandler(
            IDeviceRuntime runtime,
            IDomainEventDispatcher dispatcher,
            ICurrentUserService currentUser)
        {
            _runtime = runtime;
            _dispatcher = dispatcher;
            _currentUser = currentUser;
        }

        public async Task Handle(TurnLampOffCommand command)
        {
            var device = _runtime.GetDevice(command.DeviceId);

            device.PowerOff(_currentUser.UserId);

            await _runtime.Save(device);

            await _dispatcher.DispatchAsync(device.DomainEvents);
            device.ClearDomainEvents();
        }
    }
}
