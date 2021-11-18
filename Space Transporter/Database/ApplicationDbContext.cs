using Microsoft.EntityFrameworkCore;
using Space_Transporter.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Space_Transporter.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Ship> Ships { get; set; }

        public DbSet<Planet> Planets { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Planet>()
                .HasMany<Ship>(planet => planet.Ships)
                .WithOne(ship => ship.CurrentPlanet)
                .HasForeignKey(ship => ship.PlanetId)
                .IsRequired(true);

            //modelBuilder.Entity<Ship>()
            //    .HasOne<Planet>(ship => ship.CurrentLocation)
            //    .WithMany(planet => planet.Ships)
            //    .HasForeignKey(ship => ship.PlanetId)
            //    .IsRequired(true);
        }
    }
}
