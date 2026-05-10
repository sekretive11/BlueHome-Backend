using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Domain.Entities
{
    public class UserLog
    {
        public int UsersLogId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int SpaceId { get; set; }
        public IoTSpace Space { get; set; } = null!;

        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
