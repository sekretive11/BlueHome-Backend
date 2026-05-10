using BlueHome.Server.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Domain.Devices
{
    public readonly struct LampBrightness
    {
        public int Value { get; }

        public LampBrightness(int value)
        {
            if (value < 1 || value > 99)
                throw new DomainException("Brightness must be in range 1–99.");

            Value = value;
        }

        public static implicit operator int(LampBrightness brightness)
            => brightness.Value;

        public override string ToString()
            => Value.ToString();
    }
}
