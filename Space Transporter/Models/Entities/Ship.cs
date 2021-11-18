using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Space_Transporter.Models.Entities
{
    public class Ship
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public float MaxWarpSpeed { get; set; }

        public bool IsDocked { get; set; }

        public Planet CurrentPlanet { get; set; }

        public long PlanetId { get; set; }
    }
}
