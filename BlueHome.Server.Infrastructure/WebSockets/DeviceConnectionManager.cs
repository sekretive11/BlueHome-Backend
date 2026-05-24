using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace BlueHome.Server.Infrastructure.WebSockets
{
    public class DeviceConnectionManager
    {
        private readonly ConcurrentDictionary<int, WebSocket> _connections = new();

        public void Add(int deviceId, WebSocket socket)
        {
            _connections[deviceId] = socket;
            Console.WriteLine($"REGISTER deviceId={deviceId}");
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
    }
}
