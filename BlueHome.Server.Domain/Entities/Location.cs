using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Domain.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; } = null!;

        public List<Device> Devices { get; set; } = new();

        private Location() { }

        public static Location Create(string name)
        {
            return new Location
            {
                LocationName = name
            };
        }
    }
}
