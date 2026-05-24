using BlueHome.Server.Application.DTO;
using BlueHome.Server.Infrastructure.WebSockets;
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace BlueHome.Server.API.Middleware;

public class WebSocketMiddleware
{
    private readonly RequestDelegate _next;

    public WebSocketMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, DeviceConnectionManager manager)
    {
        if (context.Request.Path != "/ws")
        {
            await _next(context);
            return;
        }

        if (!context.WebSockets.IsWebSocketRequest)
        {
            context.Response.StatusCode = 400;
            return;
        }

        Console.WriteLine("[WS] CONNECTION INIT");

        using var socket = await context.WebSockets.AcceptWebSocketAsync();

        int? deviceId = null;
        var buffer = new byte[4096];

        try
        {
            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer, CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    Console.WriteLine("[WS] CLIENT CLOSED CONNECTION");
                    break;
                }

                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                Console.WriteLine($"[WS] MSG {message}");

                var msg = JsonSerializer.Deserialize<WsMessageDTO>(message);

                if (msg?.Type == "register")
                {
                    deviceId = msg.DeviceId;

                    Console.WriteLine($"[WS] REGISTER device={deviceId}");

                    manager.Add(deviceId.Value, socket);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[WS] ERROR {ex.Message}");
        }
        finally
        {
            if (deviceId.HasValue)
                manager.Remove(deviceId.Value);

            if (socket.State == WebSocketState.Open)
            {
                await socket.CloseAsync(
                    WebSocketCloseStatus.NormalClosure,
                    "closing",
                    CancellationToken.None
                );
            }

            Console.WriteLine("[WS] DISCONNECTED");
        }
    }
}