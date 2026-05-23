using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.DTO
{
    public class WsMessageDTO
    {
        public string Type { get; set; } = default!;
        public int DeviceId { get; set; }
        public string? DeviceType { get; set; }
    }
}
