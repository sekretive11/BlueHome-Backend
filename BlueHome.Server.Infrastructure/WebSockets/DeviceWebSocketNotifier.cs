using BlueHome.Server.Application.Abstractions.WebSockets;
using BlueHome.Server.Infrastructure.WebSockets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.WebSockets
{
    public class DeviceWebSocketNotifier : IDeviceNotifier
    {
        private readonly DeviceConnectionManager _manager;

        public DeviceWebSocketNotifier(DeviceConnectionManager manager)
        {
            _manager = manager;
        }

        public Task SendLampOn(int deviceId)
        {
            return Send(deviceId, new DeviceMessage
            {
                Type = "lamp_on",
                DeviceId = deviceId
            });
        }

        public Task SendLampOff(int deviceId)
        {
            return Send(deviceId, new DeviceMessage
            {
                Type = "lamp_off",
                DeviceId = deviceId
            });
        }

        public Task SendBrightness(int deviceId, int value)
        {
            return Send(deviceId, new DeviceMessage
            {
                Type = "brightness_set",
                DeviceId = deviceId,
                Value = value
            });
        }

        private Task Send(int deviceId, DeviceMessage message)
        {
            var json = JsonSerializer.Serialize(message);
            return _manager.SendAsync(deviceId, json);
        }
    }
}
