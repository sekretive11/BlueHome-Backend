using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Abstractions.WebSockets
{
    public interface IDeviceNotifier
    {
        Task SendLampOn(int deviceId);
        Task SendLampOff(int deviceId);
        Task SendBrightness(int deviceId, int value);
    }
}
