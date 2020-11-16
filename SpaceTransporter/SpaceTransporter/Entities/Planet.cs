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

        public List<Ship> DockedShips { set; get; }

        public Planet()
        {
            DockedShips = new List<Ship>();
        }

        public int GetRemainingDockingCapacity()
        {
            return DockingCapacityLimit - DockedShips.Count();
        }
    }
}
