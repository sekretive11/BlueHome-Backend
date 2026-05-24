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
    public class DeviceMessageRouter
    {
        private readonly DeviceConnectionManager _manager;
        private readonly IDeviceNotifier _notifier;

        public DeviceMessageRouter(
            DeviceConnectionManager manager,
            IDeviceNotifier notifier)
        {
            _manager = manager;
            _notifier = notifier;
        }

        private async Task Send(int deviceId, object payload)
        {
            var json = JsonSerializer.Serialize(payload);
            await _manager.SendAsync(deviceId, json);
        }

        public async Task RouteAsync(int deviceId, string rawMessage)
        {
            var message = JsonSerializer.Deserialize<DeviceMessage>(
                rawMessage,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (message == null)
                return;

            switch (message.Type)
            {
                case "register":
                {
                    Console.WriteLine($"DEVICE REGISTERED VIA ROUTER: {deviceId}");

                    await Send(deviceId, new
                    {
                        type = "registered",
                        deviceId
                    });

                    break;
                }

                case "state":
                {
                    Console.WriteLine($"STATE UPDATE FROM DEVICE {deviceId}: {message.Value}");

                    break;
                }

                case "ping":
                {
                    await Send(deviceId, new { type = "pong" });
                    break;
                }
            }
        }
    }
}
