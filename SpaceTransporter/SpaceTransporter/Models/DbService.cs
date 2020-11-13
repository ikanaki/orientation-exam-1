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
            Database.ShipsTable.Add(ship);
            Database.SaveChanges();
        }


        public List<Planet> ReadAllPlanets()
        {
            return Database.PlanetsTable.Include(planet=>planet.DockedShip).ToList();
        }

        public List<Ship> ReadAllShips()
        {
            return Database.ShipsTable.Include(ship=>ship.CurrentLocation).ToList();
        }

        public Planet ReadPlanet(string planetName)
        {
            throw new NotImplementedException();
        }

        public Planet ReadPlanet(int planetId)
        {
            throw new NotImplementedException();
        }

        public Ship ReadShip(string shipName)
        {
            throw new NotImplementedException();
        }

        public Ship ReadShip(int shipId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePlanet(int planetIdToBeUpdated, Planet updateVlaue)
        {
            throw new NotImplementedException();
        }

        public void UpdateShip(int shipIdToBeUpdated, Ship updateValue)
        {
            throw new NotImplementedException();
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
