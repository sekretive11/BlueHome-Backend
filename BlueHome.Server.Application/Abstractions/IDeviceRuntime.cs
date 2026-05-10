using BlueHome.Server.Domain.Devices;
using BlueHome.Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Abstractions
{
    public interface IDeviceRuntime
    {
        Device GetDevice(int id);

        Task Save(Device device);

        IDeviceSession? GetSession(int id);
    }
}
