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

            if (Service.CreateNewShip(ship))
            {
                return RedirectToAction("FrontendGet");
            }
            else
            {
                var modelView = new FrontendViewModel(Service.ReadAllShips(), Service.ReadAllPlanets(), "", Service.ReadPlanet(ship.PlanetId));
                return View("Frontend", modelView);
            }
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

        [HttpDelete("/planets/{id}")]
        public ActionResult DeletePlanet([FromRoute] int? id)
        {
            if (!(id.HasValue))
            {
                return BadRequest();
            }
            var idNotNullValue = id.GetValueOrDefault();
            if (!(Service.IsPlanetInDB(idNotNullValue)))
            {
                return BadRequest($"There is on planet of id = {idNotNullValue}");
            }
            var planetToDelete = Service.ReadPlanet(idNotNullValue);
            List<Ship> remainigShips = planetToDelete.DockedShips;
            //planetToDelete.DockedShips = new List<Ship>();

            foreach (var planet in Service.ReadAllPlanets())
            {
                while ( (planet.GetRemainingDockingCapacity()>0) && remainigShips.Count()>0)
                {
                    var ship = remainigShips[0];
                    planetToDelete.DockedShips.Remove(ship);
                    planet.DockedShips.Add(ship);
                    Service.UpdatePlanet(planet);
                    remainigShips.Remove(ship);
                }
            }
            Service.DeletePlanet(planetToDelete);

            return Ok();
        }
       

        [HttpGet("/ships")]
        public ActionResult ReturnShips([FromQuery] double? warpAtLeast)
        {
            if (!warpAtLeast.HasValue)
            {
                return BadRequest();
            }
            var warpLimit = warpAtLeast.GetValueOrDefault();
            var AllShipsConstrained =
                Service.ReadAllShips()
                .Where(s => s.MaxWarpSpeed > warpLimit)
                .OrderByDescending(ship=>ship.MaxWarpSpeed).ToArray();

            // TODO Sort

            var AllShipsConstrainedDTO = Array.ConvertAll(AllShipsConstrained, item => new ShipDTO(item));

            return Ok(AllShipsConstrainedDTO);
        }
    }
}
