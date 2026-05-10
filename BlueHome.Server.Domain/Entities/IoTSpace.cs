using BlueHome.Server.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Domain.Entities
{
    public class IoTSpace
    {
        public int SpaceId { get; set; }

        public string SpaceName { get; set; } = null!;
        public string SpaceType { get; set; } = null!;

        public SpaceStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Device> Devices { get; set; } = new();
        public List<UserLog> UserLogs { get; set; } = new();

        private IoTSpace() { }

        public static IoTSpace Create(string name, string type)
        {
            return new IoTSpace
            {
                SpaceName = name,
                SpaceType = type,
                Status = SpaceStatus.active,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
