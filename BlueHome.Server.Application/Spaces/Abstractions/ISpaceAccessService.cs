using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Spaces.Abstractions
{
    public interface ISpaceAccessService
    {
        bool HasAccess(int userId, int spaceId);
        bool IsOwner(int userId, int spaceId);
    }
}
