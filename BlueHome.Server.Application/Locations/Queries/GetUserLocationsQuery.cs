using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Locations.Queries
{
    public sealed class GetUserLocationsQuery
    {
        public int UserId { get; }

        public GetUserLocationsQuery(int userId)
        {
            UserId = userId;
        }
    }
}
