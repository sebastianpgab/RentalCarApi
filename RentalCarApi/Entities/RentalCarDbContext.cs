using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wieczorna_nauka_aplikacja_webowa.Entities
{
    public class RentalCarDbContext : DbContext
    {

        private string _connectionString = "Server=SEBASTIANPGAB\\SQLEXPRESS; Database=RentalCarDb; Trusted_Connection=True";
        public DbSet<RentalCar> RentalCars { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RentalCar>().Property(r => r.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Vehicle>().Property(r => r.RegistrationNumber).IsRequired();
            modelBuilder.Entity<Address>().Property(r => r.Street).IsRequired();
            modelBuilder.Entity<Address>().Property(r => r.PostCode).IsRequired();
            modelBuilder.Entity<User>().Property(r => r.Email).IsRequired();
            modelBuilder.Entity<Role>().Property(r => r.Name).IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}