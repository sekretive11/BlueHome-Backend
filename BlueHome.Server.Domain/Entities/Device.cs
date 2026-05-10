using BlueHome.Server.Domain.Base;
using BlueHome.Server.Domain.Enums;
using BlueHome.Server.Domain.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Domain.Entities
{
    public class Device : AggregateRoot
    {
        public int DeviceId { get; set; }

        public int SpaceId { get; set; }
        public IoTSpace Space { get; set; } = null!;

        public int LocationId { get; set; }
        public Location Location { get; set; } = null!;

        public string DeviceName { get; set; } = null!;
        public DeviceStatus Status { get; set; }
        public string DeviceType { get; set; } = null!;

        [NotMapped]
        public int? Brightness { get; private set; }

        public List<EventLog> EventLogs { get; set; } = new();

        public void SetBrightness(int value)
        {
            Brightness = value;
            AddDomainEvent(new DeviceBrightnessChangedEvent(DeviceId, value, DateTime.UtcNow));
        }

        public void PowerOff()
        {
            Status = DeviceStatus.offline;
            AddDomainEvent(new DevicePoweredOffEvent(DeviceId, DateTime.UtcNow));
        }

        public void PowerOn()
        {
            Status = DeviceStatus.online;
            AddDomainEvent(new DevicePoweredOnEvent(DeviceId, DateTime.UtcNow));
        }
    }
}
