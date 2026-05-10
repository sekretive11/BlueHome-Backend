using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Commands
{
    public record TurnLampOnCommand(int DeviceId);
}
