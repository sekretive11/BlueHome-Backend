using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace BlueHome.Server.Infrastructure.WebSockets;

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

        if (socket == null)
        {
            Console.WriteLine($"[WS] NO SOCKET device={deviceId}");
            return;
        }

        if (socket.State != WebSocketState.Open)
        {
            Console.WriteLine($"[WS] SOCKET NOT OPEN device={deviceId} state={socket.State}");
            _manager.Remove(deviceId);
            return;
        }

        var json = JsonSerializer.Serialize(payload);
        var bytes = Encoding.UTF8.GetBytes(json);

        Console.WriteLine($"[WS] SEND device={deviceId} payload={json}");

        await socket.SendAsync(
            bytes,
            WebSocketMessageType.Text,
            true,
            CancellationToken.None
        );
    }
}