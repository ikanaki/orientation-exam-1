using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceTransporter.Models
{
    public class ShipMoveInput
    {
        public int? SendPlanetId { get; set; }
        public int? SendShipId { get; set; }
        public ShipMoveInput()
        {

        }
    }
}
