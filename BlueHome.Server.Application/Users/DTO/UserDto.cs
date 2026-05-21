using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Users.DTO
{
    public sealed class UserDto
    {
        public int UserId { get; }
        public string Username { get; }
        public string Email { get; }
        public int RoleId { get; }

        public UserDto(int userId, string username, string email, int roleId)
        {
            UserId = userId;
            Username = username;
            Email = email;
            RoleId = roleId;
        }
    }
}
