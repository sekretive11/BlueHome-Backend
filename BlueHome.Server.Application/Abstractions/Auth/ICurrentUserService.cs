using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Abstractions.Auth
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        int RoleId { get; }
        bool IsAuthenticated { get; }
    }
}
