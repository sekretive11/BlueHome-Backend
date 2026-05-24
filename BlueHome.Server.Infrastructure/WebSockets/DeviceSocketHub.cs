using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Text.Json;

namespace BlueHome.Server.Infrastructure.WebSockets
{
    public class DeviceSocketHub
    {
        private readonly DeviceConnectionManager _manager;

        public DeviceSocketHub(DeviceConnectionManager manager)
        {
            _manager = manager;
        }

        public async Task SendAsync(int deviceId, object payload)
        {
            var socket = _manager.Get(deviceId);
            Console.WriteLine($"GET SOCKET for {deviceId} = {(socket != null)}");

            if (socket == null || socket.State != WebSocketState.Open)
                return;

            var json = JsonSerializer.Serialize(payload);
            var bytes = Encoding.UTF8.GetBytes(json);

            Console.WriteLine($"WS SEND → device {deviceId}");

            await socket.SendAsync(
                bytes,
                WebSocketMessageType.Text,
                true,
                CancellationToken.None
            );
        }
    }
}
