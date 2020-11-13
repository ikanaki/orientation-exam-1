using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpaceTransporter.Entities;

namespace SpaceTransporter.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Ship> ShipsTable { get; set; }
        public DbSet<Planet> PlanetsTable { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Planet>()
                .HasMany<Ship>(planet => planet.DockedShips)
                .WithOne(ship => ship.CurrentLocation)
                .HasForeignKey(Ship => Ship.PlanetId);
        }
    }
}
