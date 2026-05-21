using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Devices.Queries
{
    public sealed class GetUserDevicesQuery
    {
        public int UserId { get; }

        public GetUserDevicesQuery(int userId)
        {
            UserId = userId;
        }
    }
}
