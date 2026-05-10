using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.DTO
{
    public class LampStatusDto
    {
        public Guid Id { get; init; }
        public bool IsOn { get; init; }
        public int? Brightness { get; init; }
    }
}
