﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpaceTransporter.Entities;
using SpaceTransporter.Interfaces;
using SpaceTransporter.Models;

namespace SpaceTransporter.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        public IService Service { set; get; }
        public HomeController(IService service)
        {
            Service = service;
        }


        [HttpGet("")]
        public IActionResult FrontendGet()
        {
            var modelView = new FrontendViewModel(Service.ReadAllShips(), Service.ReadAllPlanets());
            return View("Frontend",modelView);
        }

        [HttpPost("/ships")]
        public IActionResult CreateNewShipPost(CreateNewShipInput shipInput)
        {
            if (shipInput == null)
            {
                string error = "The Create new ship form has not been correctly fulfilled. Try it again!";
                var modelView = new FrontendViewModel(Service.ReadAllShips(), Service.ReadAllPlanets(),error);
                return View("Frontend", modelView);
            }
            if (
                String.IsNullOrEmpty(shipInput.Name)
                || String.IsNullOrEmpty(shipInput.Warp)
                || !shipInput.PlanetId.HasValue
                )
            {
                string error = "The Create new ship form has not been correctly fulfilled. Try it again!";
                var modelView = new FrontendViewModel(Service.ReadAllShips(), Service.ReadAllPlanets(), error);
                return View("Frontend", modelView);
            }

            Ship ship = new Ship();
            ship.Name = shipInput.Name;
            ship.MaxWarpSpeed = Convert.ToDouble(shipInput.Warp);
            ship.PlanetId = shipInput.PlanetId.GetValueOrDefault();

            Service.CreateNewShip(ship);

            return View();
        }
    }
}
