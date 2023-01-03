using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wieczorna_nauka_aplikacja_webowa.Entities;
using Wieczorna_nauka_aplikacja_webowa.Models;
using Wieczorna_nauka_aplikacja_webowa.Services;

namespace Wieczorna_nauka_aplikacja_webowa.Controllers
{
    [Route("api/rentalcar")]
    //dzięki temu atrybutowi sprawdzamy czy validacje w folderze Models są poprawne
    [ApiController]
    public class RentalCarController : ControllerBase
    {
        readonly IRentalCarService _rentalCarService;
        public RentalCarController(IRentalCarService rentalCarService)
        {
            _rentalCarService = rentalCarService;
        }

        [HttpPost]
        public ActionResult CreateRentalCar([FromBody] CreateRentalCarDto dto)
        {
            var id = _rentalCarService.Create(dto);
            return Created($"/api/carrental/{id}", null);
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<RentalCarDto>> GetAll()
        {
            var rentalCarsDtos = _rentalCarService.GetAll();
            return Ok(rentalCarsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<RentalCarDto> Get([FromRoute] long id)
        {
            var rentalCar = _rentalCarService.GetById(id);

            return Ok(rentalCar);
        }

        [HttpDelete("delete/{id}")]
        public ActionResult Delete([FromRoute] long id)
        {
            _rentalCarService.Delete(id);
            return NoContent();
        }

        [HttpPut("edit/{id}")]
        public ActionResult Edit([FromBody] EditRentalCarDto dto, [FromRoute] long id )
        {
            _rentalCarService.Update(dto, id);
            return Ok();
        }


    }
}