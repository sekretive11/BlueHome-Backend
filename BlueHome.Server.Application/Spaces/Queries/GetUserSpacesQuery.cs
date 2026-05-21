using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Spaces.Queries
{
    public sealed class GetUserSpacesQuery
    {
        public int UserId { get; }

        public GetUserSpacesQuery(int userId)
        {
            UserId = userId;
        }
    }
}
