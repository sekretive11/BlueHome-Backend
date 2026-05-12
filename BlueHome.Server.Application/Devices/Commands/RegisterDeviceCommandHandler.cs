using BlueHome.Server.Application.Abstractions.Auth;
using BlueHome.Server.Application.Abstractions.Persistence;
using BlueHome.Server.Application.Devices.DTO;
using BlueHome.Server.Application.Spaces.Abstractions;
using BlueHome.Server.Domain.Entities;
using BlueHome.Server.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Devices.Commands
{
    public sealed class RegisterDeviceCommandHandler
    {
        private readonly IBlueHomeDbContext _db;
        private readonly ICurrentUserService _currentUser;
        private readonly ISpaceAccessService _access;

        public RegisterDeviceCommandHandler(
            IBlueHomeDbContext db,
            ICurrentUserService currentUser,
            ISpaceAccessService access)
        {
            _db = db;
            _currentUser = currentUser;
            _access = access;
        }

        public async Task<DeviceDto> Handle(
            RegisterDeviceCommand command,
            CancellationToken ct)
        {
            if (!_access.HasAccess(_currentUser.UserId, command.SpaceId))
                throw new UnauthorizedAccessException("No access to this space");

            var spaceExists = await _db.Spaces
                .AnyAsync(x => x.SpaceId == command.SpaceId, ct);

            if (!spaceExists)
                throw new Exception("Space not found");

            var locationExists = await _db.Locations
                .AnyAsync(x => x.LocationId == command.LocationId, ct);

            if (!locationExists)
                throw new Exception("Location not found");

            var device = new Device
            {
                SpaceId = command.SpaceId,
                LocationId = command.LocationId,
                DeviceName = command.DeviceName,
                DeviceType = command.DeviceType,
                Status = DeviceStatus.online
            };

            _db.Devices.Add(device);
            await _db.SaveChangesAsync(ct);

            return new DeviceDto(
                device.DeviceId,
                device.DeviceName,
                device.DeviceType,
                device.SpaceId,
                device.LocationId
            );
        }
    }
}
