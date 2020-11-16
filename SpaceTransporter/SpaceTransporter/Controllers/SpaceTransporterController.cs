using System;
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
    public class SpaceTransporterController : Controller
    {
        public IService Service { set; get; }
        public SpaceTransporterController(IService service)
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

            return RedirectToAction("FrontendGet");
        }

        [HttpGet("/ships/{shipId}/dock")]
        public IActionResult DockTheShip([FromRoute] int? shipId)
        {
            if (!shipId.HasValue)
            {
                string error = "The ship id for the dock is invalid. Try it again!";
                var modelView = new FrontendViewModel(Service.ReadAllShips(), Service.ReadAllPlanets(), error);
                return View("Frontend", modelView);
            }
            var ship = Service.ReadShip(shipId.GetValueOrDefault());
            ship.IsDocked = true;
            Service.UpdateShip(ship);

            return RedirectToAction("FrontendGet");
        }

        // Yes, I know DRY principle but there is not time to refactor / optimize
        [HttpGet("/ships/{shipId}/undock")]
        public IActionResult UndockTheShip([FromRoute] int? shipId)
        {
            if (!shipId.HasValue)
            {
                string error = "The ship id for the dock is invalid. Try it again!";
                var modelView = new FrontendViewModel(Service.ReadAllShips(), Service.ReadAllPlanets(), error);
                return View("Frontend", modelView);
            }
            var ship = Service.ReadShip(shipId.GetValueOrDefault());
            ship.IsDocked = false;
            Service.UpdateShip(ship);

            return RedirectToAction("FrontendGet");
        }

        [HttpPost("/ships/move/")]
        public IActionResult MoveShipPost(ShipMoveInput shipMovement)
        {
            if (shipMovement == null)
            {
                string error = "The ship movement form has not been correctly fulfilled. Try it again!";
                var modelView = new FrontendViewModel(Service.ReadAllShips(), Service.ReadAllPlanets(), error);
                return View("Frontend", modelView);
            }
            if (
                !shipMovement.SendPlanetId.HasValue
                || !shipMovement.SendShipId.HasValue
                )
            {
                string error = "The invalid value of Planet or Ship. Try it again!";
                var modelView = new FrontendViewModel(Service.ReadAllShips(), Service.ReadAllPlanets(), error);
                return View("Frontend", modelView);
            }
            if (!Service.IsShipInDb(shipMovement.SendShipId.GetValueOrDefault()))
            {
                string error = "The ship is not in the Database. Try it again!";
                var modelView = new FrontendViewModel(Service.ReadAllShips(), Service.ReadAllPlanets(), error);
                return View("Frontend", modelView);
            }
            
            var ship = Service.ReadShip(shipMovement.SendShipId.GetValueOrDefault());
            var sourcePlanet = Service.ReadPlanet(ship.CurrentLocation.Id);
            var destinationPlanet = Service.ReadPlanet(shipMovement.SendPlanetId.GetValueOrDefault());
            if (ship.IsDocked)
            {
                string error = $"The ship {ship.Name}, requested to send, is docked! Undock and try it again!";
                var modelView = new FrontendViewModel(Service.ReadAllShips(), Service.ReadAllPlanets(), error);
                return View("Frontend", modelView);
            }
            // Now move the ship
            ship.CurrentLocation = destinationPlanet;
            Service.UpdateShip(ship);

            return RedirectToAction("FrontendGet");
        }

       

        [HttpGet("/ships")]
        public ActionResult ReturnShips([FromQuery] double? warpAtLeastInput)
        {
            if (!warpAtLeastInput.HasValue)
            {
                return BadRequest();
            }
            var warpAtLeast = warpAtLeastInput.GetValueOrDefault();
            var AllShipsConstrained =
                Service.ReadAllShips().Where(s => s.MaxWarpSpeed > warpAtLeast).ToArray();

            // TODO Sort

            return Ok(AllShipsConstrained);

        }
    }
}
