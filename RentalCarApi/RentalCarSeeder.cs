using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Wieczorna_nauka_aplikacja_webowa.Entities
{
    public class RentalCarSeeder
    {
        private readonly RentalCarDbContext _dbContext;
        public RentalCarSeeder(RentalCarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                //zwraca liste migracji, które jeszcze nie zostały zaaplikowane 
                var pendingMigrations =_dbContext.Database.GetPendingMigrations();
                if(pendingMigrations != null && pendingMigrations.Any())
                {
                    _dbContext.Database.Migrate();
                }

                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.RentalCars.Any())
                {
                    var rentalCars = GetRentalCars();
                    _dbContext.RentalCars.AddRange(rentalCars);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                 new Role()
                 {
                     Name = "Manager"
                 },
                 new Role()
                 {
                     Name = "Admin"
                 }
            };
            return roles;
        }

        public IEnumerable<RentalCar> GetRentalCars()
        {
            var rentalCars = new List<RentalCar>()
            {
                new RentalCar()
                {
                    Name = "Auta u Zbyszka",
                    Description = "Samochdowy osobowe, ciezarowe, autobusy w 3 min",
                    Abroad = false,
                    Advance = true,
                    ContactNumber = "+48 543243089",
                    ContactEmail = "zybyszekauta@wp.pl",
                    Vehicles = new List<Vehicle>()
                    {
                        new Vehicle()
                        {
                            Type = "SUV",
                            HasGas = false,
                            Price = 100,
                            HorsePower = "290",
                            HasFourWheelDrive = true,
                            FirstRegistration = new DateTime(2019,5,6),
                            RegistrationNumber ="WPI 3214"
                        },

                        new Vehicle()
                        {
                            Type = "VAN",
                            HasGas = false,
                            Price = 150,
                            HorsePower = "390",
                            HasFourWheelDrive = true,
                            FirstRegistration = new DateTime(2018,5,6),
                            RegistrationNumber ="WPI 1222"
                        },
                    },
                     Address = new Address()
                     {
                         City = "Warsaw",
                         Street = "ul. Miła 31",
                         PostCode = "05-505"
                     }
                },    
            };
            return rentalCars;
        }
    }
}