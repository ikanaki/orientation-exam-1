using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceTransporter.Entities
{
    public class Planet
    {
        [Key]
        public int Id { get; set; }
        public int DockingCapacityLimit { get; set; }
        public string Name { get; set; }

        public List<Ship> DockedShip { set; get; }

        public Planet()
        {
            DockedShip = new List<Ship>();
        }
    }
}
