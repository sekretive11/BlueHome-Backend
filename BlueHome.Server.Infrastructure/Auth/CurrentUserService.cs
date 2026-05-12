using BlueHome.Server.Application.Abstractions.Auth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.Auth
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _context;

        public CurrentUserService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public int UserId =>
            int.Parse(_context.HttpContext?.User.FindFirst("userId")?.Value ?? "0");

        public int RoleId =>
            int.Parse(_context.HttpContext?.User.FindFirst("roleId")?.Value ?? "0");

        public bool IsAuthenticated =>
            _context.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}
