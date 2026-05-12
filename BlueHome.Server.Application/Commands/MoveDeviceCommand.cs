using BlueHome.Server.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Commands
{
    public record MoveDeviceCommand(int DeviceId, MoveTargetType TargetType, int TargetId);
}
