using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordUser { get; set; } = null!;

        public List<UserLog> UserLogs { get; set; } = new();
    }
}
