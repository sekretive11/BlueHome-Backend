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
    public sealed class GetSpaceByIdQueryHandler
    {
        private readonly IBlueHomeDbContext _db;

        public GetSpaceByIdQueryHandler(IBlueHomeDbContext db)
        {
            _db = db;
        }

        public async Task<UserSpaceDto?> Handle(
            GetSpaceByIdQuery query,
            CancellationToken ct)
        {
            var hasAccess = await _db.UserLogs.AnyAsync(
                ul => ul.UserId == query.UserId &&
                      ul.SpaceId == query.SpaceId,
                ct);

            if (!hasAccess)
                return null;

            return await _db.Spaces
                .Where(s => s.SpaceId == query.SpaceId)
                .Select(s => new UserSpaceDto(
                    s.SpaceId,
                    s.SpaceName,
                    s.SpaceType,
                    s.Status.ToString(),
                    s.CreatedAt
                ))
                .SingleOrDefaultAsync(ct);
        }
    }
}
