using BlueHome.Server.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Devices
{
    public static class DeviceTypeMapper
    {
        public static string ToDisplayName(this DeviceType type)
        {
            return type switch
            {
                DeviceType.Lamp => "Лампа",
                DeviceType.Socket => "Розетка",
                DeviceType.Thermostat => "Термостат",
                DeviceType.LightSensor => "Датчик света",
                DeviceType.DoorSensor => "Датчик двери",
                DeviceType.LeakSensor => "Датчик протечки",
                DeviceType.Cornise => "Карниз",
                _ => "Неизвестно"
            };
        }
    }
}
