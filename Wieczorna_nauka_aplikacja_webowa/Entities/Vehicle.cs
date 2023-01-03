using System;

namespace Wieczorna_nauka_aplikacja_webowa.Entities
{
    public class Vehicle
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public bool HasGas { get; set; }
        public string HorsePower { get; set; }
        public bool HasFourWheelDrive { get; set; }
        public DateTime FirstRegistration { get; set; }
        public string RegistrationNumber { get; set; }
        public long RentalCarId { get; set; }
        public virtual RentalCar RentalCar { get; set; } 

    }
}