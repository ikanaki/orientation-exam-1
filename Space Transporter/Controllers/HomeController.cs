using Microsoft.AspNetCore.Mvc;
using Space_Transporter.Models.Entities;
using Space_Transporter.Services;
using Space_Transporter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Space_Transporter.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private ShipService ShipService { get; }

        public HomeController(ShipService shipService)
        {
            ShipService = shipService;
        }

        [HttpGet("")]
        public IActionResult Home(string errorMessage = "")
        {
            return DefaultView(errorMessage);
        }

        [HttpGet("{id}")]
        public IActionResult Docking([FromRoute] long id)
        {
            ShipService.Docking(ShipService.GetShipById(id));
            return RedirectToAction("Home", "Home");
        }

        [HttpPost("send")]
        public IActionResult SendTheShip(long shipId, long planetId)
        {
            var ship = ShipService.GetShipById(shipId);
            var planet = ShipService.GetPlanetById(planetId);

            if (ship is null || ship.IsDocked == true)
            {
                return View("Home");
            }

            if (ShipService.IsPlanetReachable(planet) == false)
            {
                return DefaultView($"Docking capacity on planet {planet.Name} reached!");
            }

            ShipService.Travelling(ship, planet);
            return RedirectToAction("Home", "Home");
        }

        [HttpGet("ships")]
        public object WarpAtLeast([FromQuery] float warpAtLeast)
        {
            var ships = ShipService.GetAllShips().Where(ship => ship.MaxWarpSpeed >= warpAtLeast)
                                            .OrderByDescending(ship => ship.MaxWarpSpeed).ToList();
            return ships.Select(ship => new
            {
                name = ship.Name,
                id = ship.Id,
                location = ship.CurrentPlanet.Name,
                maximumWarp = ship.MaxWarpSpeed,
                docked = ship.IsDocked
            });
        }

        [HttpPost("ships")]
        public IActionResult AddNewShip(Ship ship)
        {
            var planet = ShipService.GetPlanetById(ship.PlanetId);
            if (ShipService.IsPlanetReachable(planet) == false)
            {
                return View("Home");
            }

            ShipService.Creating(ship);
            return RedirectToAction("Home", "Home");
        }

        [HttpDelete("planets/{id}")]
        public IActionResult DeletePlanet([FromRoute]long id)
        {
            var planet = ShipService.GetPlanetById(id);
            if (planet is null)
            {
                return StatusCode(404);
            }

            ShipService.Destroying(planet);
            return StatusCode(204);
        }

        private IActionResult DefaultView(string errorMessage = "")
        {
            return View("Home", new HomeViewModel()
            {
                AllShips = ShipService.GetAllShips(),
                AvailableShips = ShipService.GetAvailableShips(),
                AllPlanets = ShipService.GetAllPlanets(),
                AvailablePlanets = ShipService.GetAvailablePlanets(),
                ErrorMessage = errorMessage
            });
        }
    }
}
