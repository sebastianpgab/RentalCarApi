using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wieczorna_nauka_aplikacja_webowa.Controllers.Services;
using Wieczorna_nauka_aplikacja_webowa.Models;
using Wieczorna_nauka_aplikacja_webowa.Services;

namespace Wieczorna_nauka_aplikacja_webowa.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateAddressDto createAddressDto)
        {
            var result = _addressService.CreateAddress(createAddressDto);
            return Ok(200);
        }
    }
}