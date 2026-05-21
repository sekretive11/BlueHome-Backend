using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Devices.DTO
{
    public sealed class DeviceDetailsDto
    {
        public int DeviceId { get; }
        public int SpaceId { get; }
        public int? LocationId { get; }
        public string DeviceName { get; }
        public string DeviceType { get; }
        public string Status { get; }
        public bool? IsOn { get; }
        public int? Brightness { get; }

        public DeviceDetailsDto(
            int deviceId,
            int spaceId,
            int? locationId,
            string deviceName,
            string deviceType,
            string status,
            bool? isOn,
            int? brightness)
        {
            DeviceId = deviceId;
            SpaceId = spaceId;
            LocationId = locationId;
            DeviceName = deviceName;
            DeviceType = deviceType;
            Status = status;
            IsOn = isOn;
            Brightness = brightness;
        }
    }
}
