using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceTransporter.Entities;

namespace SpaceTransporter.Models
{
    public class APIOutput
    {
        Ship[] ShipsToReturn { get; set; }
        public APIOutput(Ship[] shipsToReturn)
        {
            ShipsToReturn = shipsToReturn;
        }
    }
}
