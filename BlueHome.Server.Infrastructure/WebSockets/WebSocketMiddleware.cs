using Microsoft.AspNetCore.Http;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlueHome.Server.Application.DTO;

namespace BlueHome.Server.Infrastructure.WebSockets
{
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate _next;

        public WebSocketMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, DeviceConnectionManager manager)
        {
            Console.WriteLine("WS MIDDLEWARE HIT");

            if (!context.WebSockets.IsWebSocketRequest)
            {
                await _next(context);
                return;
            }

            var socket = await context.WebSockets.AcceptWebSocketAsync();

            int? deviceId = null;
            var buffer = new byte[1024 * 4];

            try
            {
                while (socket.State == WebSocketState.Open)
                {
                    var result = await socket.ReceiveAsync(buffer, CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Close)
                        break;

                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                    var msg = JsonSerializer.Deserialize<WsMessageDTO>(message);

                    if (msg?.Type == "register")
                    {
                        deviceId = msg.DeviceId;
                        if (deviceId.HasValue)
                            manager.Add(deviceId.Value, socket);
                    }
                }
            }
            finally
            {
                if (socket.State == WebSocketState.Open)
                {
                    await socket.CloseAsync(
                        WebSocketCloseStatus.NormalClosure,
                        "Closing",
                        CancellationToken.None);
                }

                if (deviceId.HasValue)
                    manager.Remove(deviceId.Value);
            }
        }
    }
}