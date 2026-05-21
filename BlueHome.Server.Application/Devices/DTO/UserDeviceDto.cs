using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Devices.DTO
{
    public sealed class UserDeviceDto
    {
        public int DeviceId { get; }
        public int SpaceId { get; }
        public int? LocationId { get; }
        public string DeviceName { get; }
        public string Status { get; }
        public string DeviceType { get; }

        public UserDeviceDto(
            int deviceId,
            int spaceId,
            int? locationId,
            string deviceName,
            string status,
            string deviceType)
        {
            DeviceId = deviceId;
            SpaceId = spaceId;
            LocationId = locationId;
            DeviceName = deviceName;
            Status = status;
            DeviceType = deviceType;
        }
    }
}
