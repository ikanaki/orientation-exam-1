using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceTransporter.Models
{
    public class CreateNewShipInput
    {
        public string Name { get; set; }
        public string Warp { get; set; }
        public int? PlanetId { get; set; }

        public CreateNewShipInput()
        {

        }
    }
}
