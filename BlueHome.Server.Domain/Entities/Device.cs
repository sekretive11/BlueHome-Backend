using BlueHome.Server.Domain.Base;
using BlueHome.Server.Domain.Devices;
using BlueHome.Server.Domain.Enums;
using BlueHome.Server.Domain.Events;
using BlueHome.Server.Domain.Exceptions;
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
        public LampBrightness? Brightness { get; private set; } = LampBrightness.From(50);

        public List<EventLog> EventLogs { get; set; } = new();

        public void SetBrightness(int value, int userId)
        {
            if (Status != DeviceStatus.online)
                throw new DomainException("Cannot change brightness while device is OFF.");

            var newBrightness = LampBrightness.From(value);

            if (Brightness!.Value == newBrightness.Value)
                return;

            Brightness = newBrightness;

            AddDomainEvent(
                new DeviceBrightnessChangedEvent(DeviceId, Brightness.Value, userId)
            );
        }

        public void PowerOff(int userId)
        {
            Status = DeviceStatus.offline;

            AddDomainEvent(
                new DevicePoweredOffEvent(DeviceId, userId)
            );
        }

        public void PowerOn(int userId)
        {
            Status = DeviceStatus.online;

            AddDomainEvent(
                new DevicePoweredOnEvent(DeviceId, userId)
            );
        }

        public void MoveToSpace(int spaceId, int userId)
        {
            if (SpaceId == spaceId)
                return;

            SpaceId = spaceId;
            AddDomainEvent(new DeviceMovedEvent(DeviceId, "space", spaceId, userId));
        }

        public void MoveToLocation(int locationId, int userId)
        {
            if (LocationId == locationId)
                return;

            LocationId = locationId;
            AddDomainEvent(new DeviceMovedEvent(DeviceId, "location", locationId, userId));
        }
    }
}
