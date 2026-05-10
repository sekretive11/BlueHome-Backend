using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Devices.DTO
{
    public sealed record DeviceDto(
        int Id,
        string Name,
        string Type,
        int SpaceId,
        int LocationId
    );
}
