using System.ComponentModel.DataAnnotations;

namespace Wieczorna_nauka_aplikacja_webowa.Models
{
    public class CreateRentalCarDto
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        public bool Abroad { get; set; }
        public bool Advance { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
    }
}