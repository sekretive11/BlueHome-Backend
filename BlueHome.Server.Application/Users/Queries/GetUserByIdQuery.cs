using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Users.Queries
{
    public sealed class GetUserByIdQuery
    {
        public int UserId { get; }

        public GetUserByIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
