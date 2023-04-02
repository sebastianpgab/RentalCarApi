using Microsoft.AspNetCore.Mvc;
using RentalCarAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCarAPI.Controllers
{
    [Route("api/rentalcar/info")]
    [ApiController]
    public class RentalUserController : ControllerBase
    {
        private readonly IRentalCarService rentalCarService;
        public RentalUserController(IRentalCarService _rentalCarService)
        {
            rentalCarService = _rentalCarService;
        }

       /* [HttpPost]
        public ActionResult CreateRentalUser([FromBody] CreateRentalUserDto dto)
        {
            rentalCarService.Create(dto);
        }*/


    }
}
