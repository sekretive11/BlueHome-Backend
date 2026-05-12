using BlueHome.Server.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public SpaceRole Role { get; set; }

        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
