using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

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
            if (!context.WebSockets.IsWebSocketRequest)
            {
                await _next(context);
                return;
            }

            var socket = await context.WebSockets.AcceptWebSocketAsync();

            int? deviceId = null;

            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer, CancellationToken.None);

                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                if (message.Contains("register"))
                {
                    var idStart = message.IndexOf("deviceId") + 11;
                    var idEnd = message.IndexOf("}", idStart);

                    var idStr = message.Substring(idStart, idEnd - idStart);

                    deviceId = int.Parse(idStr);

                    manager.Add(deviceId.Value, socket);
                }
            }

            if (deviceId.HasValue)
                manager.Remove(deviceId.Value);
        }
    }
}
