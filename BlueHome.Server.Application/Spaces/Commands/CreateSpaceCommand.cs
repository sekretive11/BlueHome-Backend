using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Spaces.Commands
{
    public sealed record CreateSpaceCommand(int UserId, string Name, string Type);
}
