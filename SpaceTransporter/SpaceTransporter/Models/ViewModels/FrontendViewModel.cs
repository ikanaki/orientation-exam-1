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
        public string ErrorMessage { get; set; }
        public Planet DockingCapacityReached { get; set; }
        public FrontendViewModel(List<Ship> shipsToShow, List<Planet> planetsToShow, string errorMessage="", Planet dockingCapacityReached = null)
        {
            ShipsToShow = shipsToShow;
            PlanetsToShow = planetsToShow;
            ErrorMessage = errorMessage;
            DockingCapacityReached = dockingCapacityReached;
        }
    }
}
