using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Domain.Enums
{
    public enum SpaceStatus
    {
        active,
        inactive,
        archived,
        maintenance
    }

    public enum DeviceStatus
    {
        online,
        offline,
        error,
        disabled,
        updating
    }

    public enum DeviceType
    {
        Lamp,
        Socket,
        Thermostat,
        LightSensor,
        DoorSensor,
        LeakSensor,
        Cornise
    }
}
