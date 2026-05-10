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
                .First(d => d.DeviceId == deviceId);

            return new DeviceSession(device);
        }
    }
}
