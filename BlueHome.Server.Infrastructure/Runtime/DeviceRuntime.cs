using BlueHome.Server.Application.Abstractions.Persistence;
using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Domain.Devices;
using BlueHome.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.Runtime
{
    public class DeviceRuntime : IDeviceRuntime
    {
        private readonly DeviceSessionCache _cache;
        private readonly DeviceSessionFactory _factory;
        private readonly IBlueHomeDbContext _db;

        public DeviceRuntime(
            DeviceSessionCache cache,
            DeviceSessionFactory factory,
            IBlueHomeDbContext db)
        {
            _cache = cache;
            _factory = factory;
            _db = db;
        }

        public Device GetDevice(int id)
        {
            var session = GetOrCreate(id);
            session.Touch();

            return session.Device;
        }

        public async Task Save(Device device)
        {
            var session = GetOrCreate(device.DeviceId);

            session.MarkDirty();
            session.Touch();

            _db.Devices.Update(device);
            await _db.SaveChangesAsync();
        }

        public IDeviceSession? GetSession(int id)
        {
            return _cache.TryGet(id, out var session)
                ? session
                : null;
        }

        private IDeviceSession GetOrCreate(int id)
        {
            if (_cache.TryGet(id, out var session))
                return session;

            session = _factory.Create(id);
            _cache.Set(id, session);

            return session;
        }
    }
}
