using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Application.Abstractions.Persistence;
using BlueHome.Server.Domain.Devices;
using BlueHome.Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.Persistence
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly IBlueHomeDbContext _db;

        public DeviceRepository(IBlueHomeDbContext db)
        {
            _db = db;
        }

        public Device? GetById(int id)
        {
            return _db.Devices.FirstOrDefault(d => d.DeviceId == id);
        }

        public async void Update(Device device)
        {
            _db.Devices.Update(device);
            await _db.SaveChangesAsync();
        }
    }
}
