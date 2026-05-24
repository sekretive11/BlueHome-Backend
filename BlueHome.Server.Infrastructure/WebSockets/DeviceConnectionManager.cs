using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace BlueHome.Server.Infrastructure.WebSockets;

public class DeviceConnectionManager
{
    private readonly ConcurrentDictionary<int, WebSocket> _connections = new();

    public void Add(int deviceId, WebSocket socket)
    {
        _connections[deviceId] = socket;

        Console.WriteLine($"[WS] ADD device={deviceId}");
        Console.WriteLine($"[WS] COUNT={_connections.Count}");
    }

    public void Remove(int deviceId)
    {
        _connections.TryRemove(deviceId, out _);

        Console.WriteLine($"[WS] REMOVE device={deviceId}");
    }

    public WebSocket? Get(int deviceId)
    {
        _connections.TryGetValue(deviceId, out var socket);

        var alive = socket != null && socket.State == WebSocketState.Open;

        Console.WriteLine($"[WS] GET device={deviceId} alive={alive}");

        return alive ? socket : null;
    }
}