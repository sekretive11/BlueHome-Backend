using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Application.Abstractions.Auth;
using BlueHome.Server.Application.Abstractions.Persistence;
using BlueHome.Server.Application.Commands;
using BlueHome.Server.Application.Spaces.Abstractions;
using BlueHome.Server.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.CommandHandlers
{
    public class MoveDeviceHandler
    {
        private readonly IBlueHomeDbContext _db;
        private readonly IDeviceRuntime _runtime;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICurrentUserService _currentUser;
        private readonly ISpaceAccessService _access;

        public MoveDeviceHandler(
            IBlueHomeDbContext db,
            IDeviceRuntime runtime,
            IEventPublisher eventPublisher,
            ICurrentUserService currentUser,
            ISpaceAccessService access)
        {
            _db = db;
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
                {
                    var location = await _db.Locations
                        .FirstOrDefaultAsync(x => x.LocationId == command.TargetId);

                    if (location == null)
                        throw new Exception("Location not found");

                    if (location.SpaceId != device.SpaceId)
                        throw new UnauthorizedAccessException("Location does not belong to device space");

                    device.MoveToLocation(location.LocationId, _currentUser.UserId);
                    break;
                }
            }

            await _runtime.Save(device);

            _eventPublisher.Publish(device.DomainEvents);
            device.ClearDomainEvents();
        }
    }
}
