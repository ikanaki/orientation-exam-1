using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaceTransporter.Entities;

namespace SpaceTransporter.Models
{
    public class FrontendViewModel
    {
        public List<Ship> ShipsToShow { get; set; }
        public List<Planet> PlanetsToShow{ get; set; }
        public FrontendViewModel(List<Ship> shipsToShow, List<Planet> planetsToShow)
        {
            ShipsToShow = shipsToShow;
            PlanetsToShow = planetsToShow;
        }

    }
}
