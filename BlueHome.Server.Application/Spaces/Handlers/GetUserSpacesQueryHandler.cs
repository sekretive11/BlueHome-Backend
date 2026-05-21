using BlueHome.Server.Application.Abstractions.Persistence;
using BlueHome.Server.Application.Spaces.DTO;
using BlueHome.Server.Application.Spaces.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Spaces.Handlers
{
    public class GetUserSpacesQueryHandler
    {
        private readonly IBlueHomeDbContext _db;

        public GetUserSpacesQueryHandler(IBlueHomeDbContext db)
        {
            _db = db;
        }

        public async Task<List<UserSpaceDto>> Handle(
            GetUserSpacesQuery query,
            CancellationToken cancellationToken)
        {
            return await _db.UserLogs
                .Where(x => x.UserId == query.UserId)
                .Select(x => x.Space)
                .Distinct()
                .Select(space => new UserSpaceDto(
                    space.SpaceId,
                    space.SpaceName,
                    space.SpaceType,
                    space.Status.ToString(),
                    space.CreatedAt
                ))
                .ToListAsync(cancellationToken);
        }
    }
}
