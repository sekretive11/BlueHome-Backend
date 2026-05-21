using BlueHome.Server.Application.Abstractions.Security;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        public bool IsHashed(string value)
        {
            return value.StartsWith("$2a$") ||
                   value.StartsWith("$2b$") ||
                   value.StartsWith("$2y$");
        }
    }
}
