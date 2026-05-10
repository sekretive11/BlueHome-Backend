using BlueHome.Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.Runtime
{
    public interface IDeviceSession
    {
        Device Device { get; }
        DateTime LastAccess { get; }

        void Touch();
        void MarkDirty();
        bool IsDirty { get; }
    }
}
