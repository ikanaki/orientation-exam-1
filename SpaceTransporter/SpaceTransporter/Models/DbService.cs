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

        public void CreateNewShip(Ship ship)
        {
            var planet = ReadPlanet(ship.PlanetId);
            planet.DockedShips.Add(ship);
            Database.SaveChanges();
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

        public void UpdatePlanet(int planetIdToBeUpdated, Planet updateVlaue)
        {
            throw new NotImplementedException();
        }

        public void UpdateShip(Ship updateValue)
        {
            Database.ShipsTable.Update(updateValue);
            Database.SaveChanges();
        }
        public void DeletePlanet(Planet planet)
        {
            throw new NotImplementedException();
        }

        public void DeleteShip(Ship ship)
        {
            throw new NotImplementedException();
        }
    }
}
