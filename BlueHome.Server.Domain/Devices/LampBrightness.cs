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
        public const int Min = 1;
        public const int Max = 100;

        public int Value { get; }

        public LampBrightness(int value)
        {
            if (value < Min || value > Max)
                throw new DomainException($"Brightness must be in range {Min}–{Max}.");

            Value = value;
        }

        public static LampBrightness From(int value)
            => new LampBrightness(value);

        public LampBrightness Increase(int step)
            => new LampBrightness(Math.Min(Value + step, Max));

        public LampBrightness Decrease(int step)
            => new LampBrightness(Math.Max(Value - step, Min));

        public static implicit operator int(LampBrightness b) => b.Value;
    }
}
