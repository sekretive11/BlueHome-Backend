using BlueHome.Server.Application.Abstractions.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.Runtime
{
    public class DeviceSessionFactory
    {
        private readonly IBlueHomeDbContext _db;

        public DeviceSessionFactory(IBlueHomeDbContext db)
        {
            _db = db;
        }

        public DeviceSession Create(int deviceId)
        {
            var device = _db.Devices
                .FirstOrDefault(d => d.DeviceId == deviceId);

            if (device == null)
                throw new Exception($"Device {deviceId} not found");

            return new DeviceSession(device);
        }
    }
}
