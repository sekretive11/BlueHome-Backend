using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.WebSockets.Publishers
{
    public class DeviceEventWsPublisher :
        IDeviceEventWsPublisher<DevicePoweredOnEvent>,
        IDeviceEventWsPublisher<DevicePoweredOffEvent>,
        IDeviceEventWsPublisher<DeviceBrightnessChangedEvent>
    {
        private readonly DeviceSocketHub _hub;

        public DeviceEventWsPublisher(DeviceSocketHub hub)
        {
            _hub = hub;
        }

        public async Task Publish(DevicePoweredOnEvent e)
        {
            await _hub.SendAsync(e.DeviceId, new
            {
                type = "device_on",
                deviceId = e.DeviceId,
                userId = e.UserId,
                timestamp = e.OccurredAt
            });
        }

        public async Task Publish(DevicePoweredOffEvent e)
        {
            await _hub.SendAsync(e.DeviceId, new
            {
                type = "device_off",
                deviceId = e.DeviceId,
                userId = e.UserId,
                timestamp = e.OccurredAt
            });
        }

        public async Task Publish(DeviceBrightnessChangedEvent e)
        {
            await _hub.SendAsync(e.DeviceId, new
            {
                type = "brightness_changed",
                deviceId = e.DeviceId,
                brightness = e.Brightness,
                userId = e.UserId,
                timestamp = e.OccurredAt
            });
        }
    }
}
