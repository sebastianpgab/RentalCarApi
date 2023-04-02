using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wieczorna_nauka_aplikacja_webowa.Entities
{
    public class RentalCarInfo
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //oznaczenie ? - dzięki temu data może być nullem
        public string RentalCarNameId { get; set; }
        public string RentedCarId { get; set; }
        public DateTime? RentalStartDate { get; set; }
        public DateTime? RentalEndDate { get; set; }
        public decimal PriceForAllDays { get; set; }
        public virtual RentalCar RentalCar { get; set; }
        public virtual List<Vehicle> Vehicles { get; set; }
    }
}