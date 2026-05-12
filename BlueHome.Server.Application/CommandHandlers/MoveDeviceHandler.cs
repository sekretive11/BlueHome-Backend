using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Application.Abstractions.Auth;
using BlueHome.Server.Application.Commands;
using BlueHome.Server.Application.Spaces.Abstractions;
using BlueHome.Server.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.CommandHandlers
{
    public class MoveDeviceHandler
    {
        private readonly IDeviceRuntime _runtime;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICurrentUserService _currentUser;
        private readonly ISpaceAccessService _access;

        public MoveDeviceHandler(
            IDeviceRuntime runtime,
            IEventPublisher eventPublisher,
            ICurrentUserService currentUser,
            ISpaceAccessService access)
        {
            _runtime = runtime;
            _eventPublisher = eventPublisher;
            _currentUser = currentUser;
            _access = access;
        }

        public async Task Handle(MoveDeviceCommand command)
        {
            var device = _runtime.GetDevice(command.DeviceId);

            if (!_access.HasAccess(_currentUser.UserId, device.SpaceId))
                throw new UnauthorizedAccessException("No access to device space");

            switch (command.TargetType)
            {
                case MoveTargetType.Space:

                    if (!_access.HasAccess(_currentUser.UserId, command.TargetId))
                        throw new UnauthorizedAccessException("No access to target space");

                    device.MoveToSpace(command.TargetId, _currentUser.UserId);
                    break;

                case MoveTargetType.Location:
                    device.MoveToLocation(command.TargetId, _currentUser.UserId);
                    break;
            }

            await _runtime.Save(device);

            _eventPublisher.Publish(device.DomainEvents);
            device.ClearDomainEvents();
        }
    }
}
