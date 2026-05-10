using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Spaces.DTO
{
    public sealed record SpaceDto(int Id, string Name, string Type);
}
