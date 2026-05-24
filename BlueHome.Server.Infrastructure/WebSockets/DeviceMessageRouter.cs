using BlueHome.Server.Infrastructure.WebSockets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.WebSockets
{
    public class DeviceMessageRouter
    {
        private readonly DeviceConnectionManager _manager;

        public DeviceMessageRouter(DeviceConnectionManager manager)
        {
            _manager = manager;
        }

        public async Task RouteAsync(int deviceId, string rawMessage)
        {
            var message = JsonSerializer.Deserialize<DeviceMessage>(rawMessage);

            if (message == null)
                return;

            switch (message.Type)
            {
                case "register":
                    break;

                case "state":
                    break;
            }

            await Task.CompletedTask;
        }
    }
}
