using System;
using System.Collections.Concurrent;
using BlueHome.Server.Application.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.Runtime
{
    public class DeviceSessionCache
    {
        private readonly ConcurrentDictionary<int, IDeviceSession> _cache = new();

        public bool TryGet(int deviceId, out IDeviceSession session)
            => _cache.TryGetValue(deviceId, out session!);

        public void Set(int deviceId, IDeviceSession session)
            => _cache[deviceId] = session;

        public void Remove(int deviceId)
            => _cache.TryRemove(deviceId, out _);

        public IEnumerable<IDeviceSession> GetAll()
            => _cache.Values;
    }
}
