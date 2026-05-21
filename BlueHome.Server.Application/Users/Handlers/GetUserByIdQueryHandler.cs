using BlueHome.Server.Application.Abstractions.Persistence;
using BlueHome.Server.Application.Users.DTO;
using BlueHome.Server.Application.Users.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Users.Handlers
{
    public sealed class GetUserByIdQueryHandler
    {
        private readonly IBlueHomeDbContext _db;

        public GetUserByIdQueryHandler(IBlueHomeDbContext db)
        {
            _db = db;
        }

        public async Task<UserDto> Handle(
            GetUserByIdQuery query,
            CancellationToken ct)
        {
            var user = await _db.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == query.UserId, ct);

            if (user == null)
                throw new Exception("User not found");

            return new UserDto(
                user.UserId,
                user.Username,
                user.Email,
                user.RoleId
            );
        }
    }
}
