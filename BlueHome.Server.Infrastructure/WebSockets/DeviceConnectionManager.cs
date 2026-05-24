using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.WebSockets
{
    public class DeviceConnectionManager
    {
        private readonly ConcurrentDictionary<int, WebSocket> _connections = new();

        public void Add(int deviceId, WebSocket socket)
        {
            _connections[deviceId] = socket;
        }

        public void Remove(int deviceId)
        {
            _connections.TryRemove(deviceId, out _);
        }

        public WebSocket? Get(int deviceId)
        {
            _connections.TryGetValue(deviceId, out var socket);
            return socket;
        }

        public async Task SendAsync(int deviceId, string message)
        {
            if (!_connections.TryGetValue(deviceId, out var socket))
                return;

            if (socket.State != WebSocketState.Open)
                return;

            var buffer = Encoding.UTF8.GetBytes(message);

            Console.WriteLine($"Sending WS message to {deviceId}: {message}");

            await socket.SendAsync(
                new ArraySegment<byte>(buffer),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None);
        }
    }
}
