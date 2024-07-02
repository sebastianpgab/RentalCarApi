using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wieczorna_nauka_aplikacja_webowa.Entities;
using Wieczorna_nauka_aplikacja_webowa.Models;

namespace Wieczorna_nauka_aplikacja_webowa
{
    public class RentalCarMappingProfile : Profile
    {
        public RentalCarMappingProfile()
        {
            //mapowanie z typu RentalCar do typu RentalCarDto
            CreateMap<RentalCar, RentalCarDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.PostCode, c => c.MapFrom(s => s.Address.PostCode));
            //jeśli typy i nazwy właściwości między dwoma klasami w moim przypadku ( RentalCar oraz RentalCarDto)
            //się nie różnią to AutoMapper zmappuje za nas automatycznie i
            //nie trzeba będzie pisać tego mapowania co jest wyżej.

            CreateMap<Vehicle, VehicleDto>();
            CreateMap<VehicleDto, Vehicle>();
            CreateMap<Vehicle, EditVehicleDto>();
            CreateMap<CreateAddressDto, Address>();




            //mapowanie ale z dodaniem do tabeli.
            CreateMap<CreateRentalCarDto, RentalCar>()
                .ForMember(m => m.Address, c => c.MapFrom(dto => new Address()
                {
                    City = dto.City,
                    PostCode = dto.PostCode,
                    Street = dto.Street
                }));
            CreateMap<CreateVehicleDto, Vehicle>();



    }
    }
}