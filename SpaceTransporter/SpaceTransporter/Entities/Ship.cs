using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceTransporter.Entities
{
    public class Ship
    {
        [Key]
        public int Id { get; set; }
        public double MaxWarpSpeed { get; set; }
        public bool IsDocked { get; set; }
        public string Name { get; set; }

        public int PlanetId { get; set; }
        public Planet CurrentLocation { get; set; }
    }
}
