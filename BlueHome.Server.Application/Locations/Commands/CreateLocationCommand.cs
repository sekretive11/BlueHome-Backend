using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Locations.Commands
{
    public record CreateLocationCommand(string Name, int SpaceId);
}
