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
    public class TurnLampOnHandler
    {
        private readonly IDeviceRuntime _runtime;
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly ICurrentUserService _currentUser;

        public TurnLampOnHandler(
            IDeviceRuntime runtime,
            IDomainEventDispatcher dispatcher,
            ICurrentUserService currentUser)
        {
            _runtime = runtime;
            _dispatcher = dispatcher;
            _currentUser = currentUser;
        }

        public async Task Handle(TurnLampOnCommand command)
        {
            var device = _runtime.GetDevice(command.DeviceId);

            device.PowerOn(_currentUser.UserId);

            Console.WriteLine($"DOMAIN EVENTS COUNT: {device.DomainEvents.Count}");

            await _runtime.Save(device);

            var events = device.DomainEvents.ToList();

            await _dispatcher.DispatchAsync(events);

            Console.WriteLine("HANDLER: TurnLampOn");
            device.ClearDomainEvents();
        }
    }
}
