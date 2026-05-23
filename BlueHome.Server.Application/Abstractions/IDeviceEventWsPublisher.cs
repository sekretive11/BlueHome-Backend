using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Abstractions
{
    public interface IDeviceEventWsPublisher<TEvent>
    {
        Task Publish(TEvent @event);
    }
}
