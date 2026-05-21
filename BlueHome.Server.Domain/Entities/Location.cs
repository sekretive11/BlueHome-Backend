using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Domain.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; } = null!;

        [Column("space_id")]
        public int SpaceId { get; set; }
        public IoTSpace Space { get; set; } = null!;

        public List<Device> Devices { get; set; } = new();

        private Location() { }

        public static Location Create(string name, int spaceId)
        {
            return new Location
            {
                LocationName = name,
                SpaceId = spaceId
            };
        }
    }
} 

