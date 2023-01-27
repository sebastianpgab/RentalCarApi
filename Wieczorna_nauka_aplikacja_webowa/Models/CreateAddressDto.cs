using System.ComponentModel.DataAnnotations;

namespace Wieczorna_nauka_aplikacja_webowa.Models
{
    public class CreateAddressDto
    {
        public long Id { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string PostCode { get; set; }
    }
}

