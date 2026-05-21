using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Devices.Queries
{
    public sealed class GetDeviceByIdQuery
    {
        public int DeviceId { get; }
        public int UserId { get; }

        public GetDeviceByIdQuery(int deviceId, int userId)
        {
            DeviceId = deviceId;
            UserId = userId;
        }
    }
}
