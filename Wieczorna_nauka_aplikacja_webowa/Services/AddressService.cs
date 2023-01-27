using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Wieczorna_nauka_aplikacja_webowa.Authorization;
using Wieczorna_nauka_aplikacja_webowa.Entities;
using Wieczorna_nauka_aplikacja_webowa.Exceptions;
using Wieczorna_nauka_aplikacja_webowa.Models;

namespace Wieczorna_nauka_aplikacja_webowa.Services
{
    public interface IAddressService
    {
        public CreateAddressDto CreateAddress(CreateAddressDto dto);
    }
    public class AddressService : IAddressService
    {
        private readonly IMapper _mapper;
        private readonly RentalCarDbContext _dbContext;

        public AddressService(IMapper mapper, RentalCarDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
     
        public CreateAddressDto CreateAddress(CreateAddressDto dto)
        {
            //pobieram > mapuje > dodaje;

            var address = _mapper.Map<Address>(dto);
            _dbContext.Addresses.Add(address);
            _dbContext.SaveChanges();

            return dto;

        }


    }
}