using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Wieczorna_nauka_aplikacja_webowa.Entities;
using Wieczorna_nauka_aplikacja_webowa.Models;
using Wieczorna_nauka_aplikacja_webowa.Services;

namespace Wieczorna_nauka_aplikacja_webowa.Controllers
{
    [Route("api/rentalcar")]
    //dzięki temu atrybutowi sprawdzamy czy validacje w folderze Models są poprawne
    [ApiController]
   // [Authorize(Roles = "Admin,Manager")]
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
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var id = _rentalCarService.Create(dto);
            return Created($"/api/carrental/{id}", null);
        }

        [HttpGet]
        // [Authorize(Policy = "MailIsGmail")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<RentalCarDto>> GetAll([FromQuery] RentalCarQuery query)
        {
            var rentalCarsDtos = _rentalCarService.GetAll(query);
            return Ok(rentalCarsDtos);
        }

        [HttpGet("{id}")]
        //zapytanie bez nagłowka z Autoryzacją
        [AllowAnonymous]
        public ActionResult<RentalCarDto> Get([FromRoute] long id)
        {
            var rentalCar = _rentalCarService.GetById(id);

            return Ok(rentalCar);
        }

        [HttpDelete("delete/{id}")]
        [Authorize (Policy = "MailIsGmail")]
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