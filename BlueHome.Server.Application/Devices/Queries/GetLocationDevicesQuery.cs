using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Devices.Queries
{
    public sealed class GetLocationDevicesQuery
    {
        public int LocationId { get; }
        public int UserId { get; }

        public GetLocationDevicesQuery(int locationId, int userId)
        {
            LocationId = locationId;
            UserId = userId;
        }
    }
}
