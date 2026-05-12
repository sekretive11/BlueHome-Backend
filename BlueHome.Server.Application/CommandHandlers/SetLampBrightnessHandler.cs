using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Application.Abstractions.Auth;
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
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly ICurrentUserService _currentUser;

        public SetLampBrightnessHandler(
            IDeviceRuntime runtime,
            IDomainEventDispatcher dispatcher,
            ICurrentUserService currentUser)
        {
            _runtime = runtime;
            _dispatcher = dispatcher;
            _currentUser = currentUser;
        }

        public async Task Handle(SetLampBrightnessCommand command)
        {
            var device = _runtime.GetDevice(command.DeviceId);
            var brightness = LampBrightness.From(command.Brightness);

            device.SetBrightness(brightness, _currentUser.UserId);

            await _runtime.Save(device);

            await _dispatcher.DispatchAsync(device.DomainEvents);
            device.ClearDomainEvents();
        }
    }
}
