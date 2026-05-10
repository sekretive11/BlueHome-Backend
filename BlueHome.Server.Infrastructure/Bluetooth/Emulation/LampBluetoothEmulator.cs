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
                case LampPoweredOnEvent e:
                    Console.WriteLine($"[BT] Lamp {e.LampId} powered ON");
                    break;

                case LampPoweredOffEvent e:
                    Console.WriteLine($"[BT] Lamp {e.LampId} powered OFF (low power mode)");
                    break;

                case LampBrightnessChangedEvent e:
                    Console.WriteLine($"[BT] Lamp {e.LampId} brightness set to {e.Brightness}");
                    break;

                default:
                    Console.WriteLine("[BT] Unknown domain event");
                    break;
            }
        }
    }
}
