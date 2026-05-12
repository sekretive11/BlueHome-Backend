using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.Bluetooth.Emulation
{
    /// <summary>
    /// Эмулятор Bluetooth-умной лампы.
    /// Имитирует поведение реального устройства.
    /// </summary>
    public class LampBluetoothEmulator : IBluetoothGateway
    {
        public void Send(IDomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case DevicePoweredOnEvent e:
                    Console.WriteLine($"[BT] Device {e.DeviceId} ON");
                    break;

                case DevicePoweredOffEvent e:
                    Console.WriteLine($"[BT] Device {e.DeviceId} OFF");
                    break;

                case DeviceBrightnessChangedEvent e:
                    Console.WriteLine($"[BT] Device {e.DeviceId} brightness {e.Brightness}");
                    break;
            }
        }
    }
}
