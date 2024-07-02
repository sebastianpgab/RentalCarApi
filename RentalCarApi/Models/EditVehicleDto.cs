using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wieczorna_nauka_aplikacja_webowa.Models
{
    public class EditVehicleDto 
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public bool HasGas { get; set; }
        public string HorsePower { get; set; }
        public bool HasFourWheelDrive { get; set; }

    }
}