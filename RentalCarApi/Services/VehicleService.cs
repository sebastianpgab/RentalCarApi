using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wieczorna_nauka_aplikacja_webowa.Authorization;
using Wieczorna_nauka_aplikacja_webowa.Entities;
using Wieczorna_nauka_aplikacja_webowa.Exceptions;
using Wieczorna_nauka_aplikacja_webowa.Models;

namespace Wieczorna_nauka_aplikacja_webowa.Services.Services
{
    public interface IVehicleService
    {
        long Create(int rentalcarId, CreateVehicleDto dto);
        public List<VehicleDto> GetAll(int rentalcarId);
        public VehicleDto GetById(long rentalcarId, long dishId);
        public void RemoveById(long rentalcarId, long vehicleId);
        public void Remove(int rentalcarId);
        public EditVehicleDto Update(EditVehicleDto dto, long rentalcarId, long vehicleId);

    }
    public class VehicleService : IVehicleService
    {
        private readonly IMapper _mapper;
        private readonly RentalCarDbContext _dbContext;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public VehicleService(IMapper mapper,  RentalCarDbContext dbContext, IAuthorizationService authoraztionService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _authorizationService = authoraztionService;
            _userContextService = userContextService;
        }
        public long Create(int rentalcarId, CreateVehicleDto dto)
        {
            var vehicle = _dbContext.RentalCars.FirstOrDefault(r => r.Id == rentalcarId);
            if (vehicle is null) throw new NotFoundExceptions("RentalCar not found");
           // var vehicles = _dbContext.Vehicles;
            var vehicleEntity =_mapper.Map<Vehicle>(dto);
            vehicleEntity.CreatedById = _userContextService.GetUserId;
            vehicleEntity.RentalCarId = rentalcarId;
            _dbContext.Vehicles.Add(vehicleEntity);
            _dbContext.SaveChanges();

            return vehicleEntity.Id;
        }
        public List<VehicleDto> GetAll(int rentalcarId)
        {
            var vehicle = _dbContext.Vehicles.Where(p => p.RentalCarId == rentalcarId);
            if (vehicle is null)
            {
                throw new NotFoundExceptions("RentalCar not found");
            }

           var vehicleDto = _mapper.Map<List<VehicleDto>>(vehicle);
           return vehicleDto;
        }

        public VehicleDto GetById(long rentalcarId, long vehicleId)
        {
            var rentalCars = _dbContext.RentalCars;
            var rentalCar = rentalCars.FirstOrDefault(p => p.Id == rentalcarId);
            if(rentalCar is null)
            {
                throw new NotFoundExceptions("Not found RentalCar");
            }
            var vehicles = _dbContext.Vehicles;
            var vehicle = vehicles.FirstOrDefault(p => p.Id == vehicleId);
            if(vehicle is null || vehicle.Id != vehicleId)
            {
                throw new NotFoundExceptions("Not found Vehicle");
            }
            var vehiclesDto = _mapper.Map<VehicleDto>(vehicle);
            return vehiclesDto;
        }

        public void RemoveById(long rentalcarId, long vehicleId)
        {
            var vehicle = _dbContext.Vehicles.FirstOrDefault(p => p.Id == vehicleId && p.RentalCarId == rentalcarId);
            if(vehicle is null ) throw new NotFoundExceptions("Vehicle not found");

            //tu jest błąd !!! nie mozna usunąć, chyba coś nie tak z przekazaniem zasobu lub usera  ?
            var authorizationResult = _authorizationService
            .AuthorizeAsync(_userContextService.User/*przekazujemy zalogowanego usera*/, vehicle/*zasób*/, new ResourceOperationRequirement(ResourceOperation.Delete)).Result/*oraz informację jaki handler wykonać*/;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _dbContext.Vehicles.Remove(vehicle);
            _dbContext.SaveChanges();
        }
        public void Remove(int rentalcarId)
        {
            var vehicle = _dbContext.Vehicles.Where(p => p.RentalCarId == rentalcarId);

            if (vehicle is null) throw new NotFoundExceptions("RentalCar not found");
            
                _dbContext.RemoveRange(vehicle); //RemoveRange usuwa liste ID, Remove tylko pojedyczne ID 
                _dbContext.SaveChanges();
        }

        public EditVehicleDto Update(EditVehicleDto dto, long rentalcarId, long vehicleId)
        {
           var vehicle = _dbContext.Vehicles.FirstOrDefault(p => p.Id == vehicleId && p.RentalCarId == rentalcarId);
            if (vehicle is null) throw new NotFoundExceptions("Not found  Vehicle - UpdateVehicle");

           /* vehicle.Type = dto.Type;
            vehicle.Price = dto.Price;
            vehicle.HasGas = dto.HasGas;
            vehicle.HorsePower = dto.HorsePower;
            vehicle.HasFourWheelDrive = dto.HasFourWheelDrive;*/


            var vehicleDto = _mapper.Map<EditVehicleDto>(vehicle);
            _dbContext.SaveChanges();

            return vehicleDto;
        }



    }


}