using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Spaces.DTO
{
    public record SpaceListItemDto(
        int SpaceId,
        string SpaceName,
        string SpaceType,
        string Status,
        DateTime CreatedAt
    );
}
