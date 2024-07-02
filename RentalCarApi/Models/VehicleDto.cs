using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wieczorna_nauka_aplikacja_webowa.Models
{
    public class VehicleDto
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public bool HasGas { get; set; }
        public long? CreatedById { get; set; }
        public string HorsePower { get; set; }
        public bool HasFourWheelDrive { get; set; }
        public DateTime FirstRegistration { get; set; }
        public string RegistrationNumber { get; set; }
        public int RentalCarId { get; set; }

    }
}