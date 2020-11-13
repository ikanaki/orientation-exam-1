using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceTransporter.Entities
{
    public class Planet
    {
        public int Id { get; set; }
        public int DockingCapacityLimit { get; set; }
        public string Name { get; set; }
    }
}
