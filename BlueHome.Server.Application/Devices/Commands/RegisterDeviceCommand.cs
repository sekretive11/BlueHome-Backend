using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Devices.Commands
{
    public sealed record RegisterDeviceCommand(
        int SpaceId,
        int LocationId,
        string DeviceName,
        string DeviceType
    );
}
