using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Auth.Commands
{
    public record LoginCommand(string Email, string Password);
}
