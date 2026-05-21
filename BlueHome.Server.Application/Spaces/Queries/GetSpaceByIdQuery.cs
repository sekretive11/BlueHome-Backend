using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Spaces.Queries
{
    public sealed class GetSpaceByIdQuery
    {
        public int SpaceId { get; }
        public int UserId { get; }

        public GetSpaceByIdQuery(int spaceId, int userId)
        {
            SpaceId = spaceId;
            UserId = userId;
        }
    }
}
