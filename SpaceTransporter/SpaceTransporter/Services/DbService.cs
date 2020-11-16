using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpaceTransporter.Database;
using SpaceTransporter.Entities;
using SpaceTransporter.Interfaces;

namespace SpaceTransporter.Models
{
    public class DbService : IService
    {

        private readonly ApplicationDbContext Database;

        public DbService(ApplicationDbContext database)
        {
            Database = database;
        }

        public bool CreateNewShip(Ship ship)
        {
            var planet = ReadPlanet(ship.PlanetId);
            if (planet.GetRemainingDockingCapacity() > 0)
            {
                planet.DockedShips.Add(ship);
                Database.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }


        public List<Planet> ReadAllPlanets()
        {
            return Database.PlanetsTable.Include(planet=>planet.DockedShips).ToList();
        }

        public List<Ship> ReadAllShips()
        {
            return Database.ShipsTable.Include(ship=>ship.CurrentLocation).ToList();
        }

        public Planet ReadPlanet(string planetName)
        {
            return Database.PlanetsTable.Where(p=>p.Name==planetName).Include(p=>p.DockedShips).FirstOrDefault() ;
        }

        public Planet ReadPlanet(int planetId)
        {
            return Database.PlanetsTable.Where(p => p.Id == planetId).Include(p=>p.DockedShips).FirstOrDefault() ;
        }

        public Ship ReadShip(string shipName)
        {
            return Database.ShipsTable.Where(s=>s.Name == shipName).Include(s=>s.CurrentLocation).FirstOrDefault();
        }

        public Ship ReadShip(int shipId)
        {
            return Database.ShipsTable.Where(s=>s.Id == shipId).Include(s=>s.CurrentLocation).FirstOrDefault();
        }

        public void UpdatePlanet(Planet updateValue)
        {
            Database.PlanetsTable.Update(updateValue);
            Database.SaveChanges();
        }

        public void UpdateShip(Ship updateValue)
        {
            Database.ShipsTable.Update(updateValue);
            Database.SaveChanges();
        }
        public void DeletePlanet(Planet planet)
        {
            Database.PlanetsTable.Remove(planet);
            Database.SaveChanges();
        }

        public void DeleteShip(Ship ship)
        {
            throw new NotImplementedException();
        }

        public bool IsShipInDb(int shipID)
        {
            return Database.ShipsTable.Any(s=>s.Id == shipID);
        }

        public bool IsPlanetInDB(int planetID)
        {
            return Database.PlanetsTable.Any(s=>s.Id == planetID);
        }
    }
}
