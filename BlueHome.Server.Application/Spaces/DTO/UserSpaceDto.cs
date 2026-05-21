using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Spaces.DTO
{
    public sealed class UserSpaceDto
    {
        public int SpaceId { get; }
        public string SpaceName { get; }
        public string SpaceType { get; }
        public string Status { get; }
        public DateTime CreatedAt { get; }

        public UserSpaceDto(
            int spaceId,
            string spaceName,
            string spaceType,
            string status,
            DateTime createdAt)
        {
            SpaceId = spaceId;
            SpaceName = spaceName;
            SpaceType = spaceType;
            Status = status;
            CreatedAt = createdAt;
        }
    }
}
