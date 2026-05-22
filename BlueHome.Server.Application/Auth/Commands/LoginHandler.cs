using BlueHome.Server.Application.Abstractions.Auth;
using BlueHome.Server.Application.Abstractions.Persistence;
using BlueHome.Server.Application.Abstractions.Security;
using BlueHome.Server.Application.Auth.DTO;
using BlueHome.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Auth.Commands
{
    public class LoginHandler
    {
        private readonly IBlueHomeDbContext _db;
        private readonly JwtSettings _jwt;
        private readonly IPasswordHasher _hasher;

        public LoginHandler(IBlueHomeDbContext db, JwtSettings jwt, IPasswordHasher hasher)
        {
            _db = db;
            _jwt = jwt;
            _hasher = hasher;
        }

        public async Task<LoginResult> Handle(LoginCommand command)
        {
            var user = await _db.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == command.Email);

            if (user == null)
                throw new Exception("User not found");

            var isValid =
                _hasher.IsHashed(user.PasswordUser)
                    ? _hasher.Verify(command.Password, user.PasswordUser)
                    : user.PasswordUser == command.Password;

            if (!isValid)
                throw new Exception("Invalid password");

            /*if (!_hasher.IsHashed(user.PasswordUser))
            {
                user.PasswordUser = _hasher.Hash(user.PasswordUser);
                await _db.SaveChangesAsync();
            }*/

            var token = GenerateToken(user);

            return new LoginResult(token);
        }

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim("userId", user.UserId.ToString()),
                new Claim("roleId", user.RoleId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.RoleName)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwt.Secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwt.Issuer,
                _jwt.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.ExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
