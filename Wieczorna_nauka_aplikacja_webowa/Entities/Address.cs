namespace Wieczorna_nauka_aplikacja_webowa.Entities
{
    public class Address
    {
        public long Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public virtual RentalCar RentalCar { get; set; }

    }
}