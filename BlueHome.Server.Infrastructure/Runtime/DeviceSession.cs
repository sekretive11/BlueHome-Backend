using BlueHome.Server.Domain.Entities;
using BlueHome.Server.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.Runtime
{
    public class DeviceSession : IDeviceSession
    {
        public Device Device { get; }

        public DateTime LastAccess { get; private set; }

        public bool IsDirty { get; private set; }

        public DeviceSession(Device device)
        {
            Device = device;
            LastAccess = DateTime.UtcNow;
        }

        public void Touch()
        {
            LastAccess = DateTime.UtcNow;
        }

        public void MarkDirty()
        {
            IsDirty = true;
        }

        public void ApplyChanges(Device updated)
        {
            Device.DeviceName = updated.DeviceName;
            Device.Status = updated.Status;
            Device.DeviceType = updated.DeviceType;
        }
    }
}
