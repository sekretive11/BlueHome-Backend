using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.WebSockets.Models
{
    public class DeviceMessage
    {
        public string Type { get; set; } = null!;
        public int DeviceId { get; set; }
        public int? Value { get; set; }
    }
}
