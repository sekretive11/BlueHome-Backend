using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Locations.Queries
{
    public sealed class GetLocationByIdQuery
    {
        public int LocationId { get; }
        public int UserId { get; }

        public GetLocationByIdQuery(int locationId, int userId)
        {
            LocationId = locationId;
            UserId = userId;
        }
    }
}
