using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wieczorna_nauka_aplikacja_webowa.Entities;
using Wieczorna_nauka_aplikacja_webowa.Models;
using Wieczorna_nauka_aplikacja_webowa.Services.Services;

namespace Wieczorna_nauka_aplikacja_webowa.Controllers
{
    // główna ścieżka, aby uruchomić dane akcje w tym kontrolerze, należy ją przepisać
    //np. https://localhost:5001/api/rentalcar/10/vehicle
    [Route("api/rentalcar/{rentalcarId}/vehicle")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly RentalCarDbContext _rentalCarDb;
        private readonly IVehicleService _vehicle;
        public VehicleController(RentalCarDbContext rentalCarDb, IVehicleService vehicle)
        {
            _rentalCarDb = rentalCarDb;
            _vehicle = vehicle;
        }

        [HttpPost]
        public ActionResult CreateVehicle([FromRoute] int rentalcarId, [FromBody] CreateVehicleDto vehicle)
        {
            var newVehicleId = _vehicle.Create(rentalcarId, vehicle);
            return Created($"api/rentalcar/{rentalcarId}/vehicle/{newVehicleId}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<VehicleDto>> GetAll([FromRoute] int rentalCarId)
        {
            var allVehicles = _vehicle.GetAll(rentalCarId);
            return Ok(allVehicles);

        }
        
        [HttpGet("{vehicleId}")]
        public ActionResult<VehicleDto> Get([FromRoute] long rentalcarId, [FromRoute] long vehicleId)
        {
            var result = _vehicle.GetById(rentalcarId, vehicleId);
            return Ok(result);
        }

        [HttpDelete("{vehicleId}")]
        public ActionResult Remove([FromRoute] long rentalcarId, [FromRoute] long vehicleId)
        {
            _vehicle.RemoveById(rentalcarId, vehicleId);
            return Ok($"VehicleId was removed - {vehicleId}");
        }

       [HttpDelete]
        public ActionResult RemoveAll([FromRoute] int rentalcarId)
        {
            _vehicle.Remove(rentalcarId);
            return Ok($"Vehicles was removed");
        }

        [HttpPut("{vehicleId}")]
        public ActionResult<EditVehicleDto> Edit([FromBody] EditVehicleDto dto, [FromRoute] long rentalcarID, [FromRoute] long vehicleId)
        {
            var result = _vehicle.Update(dto, rentalcarID, vehicleId);
            return Ok(result);
        }

    }


}
