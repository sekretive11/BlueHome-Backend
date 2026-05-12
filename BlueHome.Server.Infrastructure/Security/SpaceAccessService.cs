using BlueHome.Server.Application.Abstractions.Persistence;
using BlueHome.Server.Application.Spaces.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.Security
{
    public class SpaceAccessService : ISpaceAccessService
    {
        private readonly IBlueHomeDbContext _db;

        public SpaceAccessService(IBlueHomeDbContext db)
        {
            _db = db;
        }

        public bool HasAccess(int userId, int spaceId)
        {
            return _db.UserLogs.Any(x =>
                x.UserId == userId &&
                x.SpaceId == spaceId);
        }

        public bool IsOwner(int userId, int spaceId)
        {
            return _db.UserLogs.Any(x =>
                x.UserId == userId &&
                x.SpaceId == spaceId &&
                x.Role == Domain.Enums.SpaceRole.Owner);
        }
    }
}
