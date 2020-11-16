using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceTransporter.Entities;

namespace SpaceTransporter.Models
{
    public class ShipDTO
    {
        public string  Name { get; set; }
        public int Id { get; set; }
        public string Location { get; set; }
        public  double MaximumWarp { get; set; }
        public bool Docked { get; set; }

        //public ShipDTO()
        //{

        //}

        public ShipDTO(Ship ship)
        {
            Name = ship.Name;
            Id = ship.Id;
            Location = ship.CurrentLocation.Name;
            MaximumWarp = ship.MaxWarpSpeed;
            Docked = ship.IsDocked;
        }

    }
}
