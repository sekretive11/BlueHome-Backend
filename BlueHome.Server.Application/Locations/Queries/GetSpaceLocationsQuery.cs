using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Locations.Queries
{
    public sealed class GetSpaceLocationsQuery
    {
        public int SpaceId { get; }
        public int UserId { get; }

        public GetSpaceLocationsQuery(int spaceId, int userId)
        {
            SpaceId = spaceId;
            UserId = userId;
        }
    }
}
