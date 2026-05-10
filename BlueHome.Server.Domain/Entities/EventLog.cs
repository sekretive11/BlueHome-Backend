using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Domain.Entities
{
    public class EventLog
    {
        public int EventLogId { get; set; }

        public int DeviceId { get; set; }
        public Device Device { get; set; } = null!;

        public string EventType { get; set; } = null!;
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
