using Microsoft.AspNetCore.Mvc.Rendering;
using Space_Transporter.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Space_Transporter.ViewModels
{
    public class HomeViewModel
    {
        public Ship Ship { get; set; }

        public List<Ship> AllShips { get; set; }

        public List<SelectListItem> AvailableShips { get; set; }

        public Planet Planet { get; set; }

        public List<Planet> AllPlanets { get; set; }

        public List<SelectListItem> AvailablePlanets { get; set; }

        public string ErrorMessage { get; set; }
    }
}
