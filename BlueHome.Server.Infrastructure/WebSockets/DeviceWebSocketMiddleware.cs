using BlueHome.Server.Infrastructure.WebSockets.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.WebSockets
{
    public class DeviceWebSocketMiddleware
    {
        private readonly RequestDelegate _next;

        public DeviceWebSocketMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            DeviceConnectionManager manager,
            DeviceMessageRouter router)
        {
            if (context.Request.Path != "/ws/device")
            {
                await _next(context);
                return;
            }

            if (!context.WebSockets.IsWebSocketRequest)
            {
                await _next(context);
                return;
            }

            var socket = await context.WebSockets.AcceptWebSocketAsync();

            var buffer = new byte[1024 * 4];

            var result = await socket.ReceiveAsync(
                new ArraySegment<byte>(buffer),
                CancellationToken.None);

            var json = Encoding.UTF8.GetString(buffer, 0, result.Count);

            var register = System.Text.Json.JsonSerializer.Deserialize<DeviceMessage>(json);

            if (register == null || register.Type != "register")
            {
                await socket.CloseAsync(
                    WebSocketCloseStatus.PolicyViolation,
                    "First message must be register",
                    CancellationToken.None);

                return;
            }

            var deviceId = register.DeviceId;

            manager.Add(deviceId, socket);

            Console.WriteLine($"WS REGISTER deviceId = {deviceId}");

            try
            {
                while (!socket.CloseStatus.HasValue)
                {

                    var resultMsg = await socket.ReceiveAsync(
                        new ArraySegment<byte>(buffer),
                        CancellationToken.None);

                    if (resultMsg.MessageType == WebSocketMessageType.Close)
                        break;

                    var msg = Encoding.UTF8.GetString(buffer, 0, resultMsg.Count);

                    await router.RouteAsync(deviceId, msg);
                }
            }
            finally
            {
                manager.Remove(deviceId);

                await socket.CloseAsync(
                    WebSocketCloseStatus.NormalClosure,
                    "Closed",
                    CancellationToken.None);
            }
        }
    }
}
