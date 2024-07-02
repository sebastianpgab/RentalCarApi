using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wieczorna_nauka_aplikacja_webowa.Models
{
    public class EditRentalCarDto
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
        public bool Advance { get; set; }

    }
}