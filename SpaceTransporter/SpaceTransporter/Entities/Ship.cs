using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceTransporter.Entities
{
    public class Ship
    {
        public int Id { get; set; }
        public double MaxWarpSpeed { get; set; }

        public bool IsDocked { get; set; }
        public int Name { get; set; }
    }
}
