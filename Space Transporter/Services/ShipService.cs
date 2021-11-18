using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Space_Transporter.Database;
using Space_Transporter.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Space_Transporter.Services
{
    public class ShipService
    {
        private ApplicationDbContext DbContext { get; }

        public ShipService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public Ship GetShipById(long id)
        {
            return DbContext.Ships.Find(id);
        }

        public List<Ship> GetAllShips()
        {
            return DbContext.Ships.OrderBy(ship => ship.Id)
                                  .Include(ship => ship.CurrentPlanet)
                                  .ToList();
        }

        public List<SelectListItem> GetAvailableShips()
        {
            return DbContext.Ships.Where(ship => ship.IsDocked == false)
                                  .OrderBy(ship => ship.Name)
                                  .Select(ship => new SelectListItem { 
                                      Value = Convert.ToString(ship.Id), 
                                      Text = ship.Name 
                                  }).ToList();
        }

        public void Docking(Ship ship)
        {
            ship.IsDocked = !ship.IsDocked;
            DbContext.SaveChanges();
        }

        public void Travelling(Ship ship, Planet planet)
        {
            ship.PlanetId = planet.Id;
            DbContext.SaveChanges();
        }

        public void Creating(Ship ship)
        {
            DbContext.Ships.Add(ship);
            DbContext.SaveChanges();
        }

        public void Destroying(Planet planet)
        {
            planet.Ships = GetAllShips().Where(ship => ship.PlanetId == planet.Id).ToList();
            var Random = new Random();
            var planets = GetAllPlanets().Where(x => x.Id != planet.Id).ToList();
            while (planet.Ships.Count > 0)
            {
                Travelling(planet.Ships[0], planets[Random.Next(planets.Count - 1)]);
            }
            DbContext.Planets.Remove(planet);
            DbContext.SaveChanges();
        }

        public Planet GetPlanetById(long id)
        {
            return DbContext.Planets.Find(id);
        }

        public List<Planet> GetAllPlanets()
        {
            return DbContext.Planets.OrderBy(planet => planet.Name)
                                    .Include(planet => planet.Ships)
                                    .ToList();
        }

        public List<SelectListItem> GetAvailablePlanets()
        {
            return DbContext.Planets.OrderBy(planet => planet.Name)
                                    .Select(planet => new SelectListItem
                                    {
                                        Value = Convert.ToString(planet.Id),
                                        Text = planet.Name
                                    }).ToList();
        }

        public bool IsPlanetReachable(Planet planet)
        {
            planet.Ships = GetAllShips().Where(ship => ship.PlanetId == planet.Id).ToList();
            var shipsOnPlanetCount = planet.Ships.Count;
            return shipsOnPlanetCount < planet.DockingCapacity;
        }
    }
}
