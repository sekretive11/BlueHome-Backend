using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Locations.DTO
{
    public sealed class UserLocationDto
    {
        public int LocationId { get; }
        public string LocationName { get; }
        public int SpaceId { get; }

        public UserLocationDto(int locationId, string locationName, int spaceId)
        {
            LocationId = locationId;
            LocationName = locationName;
            SpaceId = spaceId;
        }
    }
}
