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
    public interface IRentalCarService
    {
        RentalCarDto GetById(long id);
        PageResult<RentalCarDto> GetAll(RentalCarQuery query);
        long Create(CreateRentalCarDto dto);
        void Delete(long id);
        void Update(EditRentalCarDto dto, long id);

    }
    public class RentalCarService : IRentalCarService
    {
        private readonly IMapper _mapper;
        private readonly RentalCarDbContext _dbContext;
        private readonly ILogger<RentalCarService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        public RentalCarService(RentalCarDbContext dbcontext, IMapper mapper, ILogger<RentalCarService> logger, 
            IAuthorizationService authoraztionService /*dzięki temu serwisowi asp. net core na podstawie wymagania będzie wstanie wywołać odpowiedni handler*/,
            IUserContextService userContextService)
        {
            _mapper = mapper;
            _dbContext = dbcontext;
            _logger = logger;
            _authorizationService = authoraztionService;
            _userContextService = userContextService;
        }
        public RentalCarDto GetById(long id)
        {
            var rentalCar = _dbContext
               .RentalCars
               .Include(r => r.Address)
               .Include(r => r.Vehicles)
               .FirstOrDefault(r => r.Id == id);

            if(rentalCar is null) throw new NotFoundExceptions("Restaurant not found"); ;

            var result = _mapper.Map<RentalCarDto>(rentalCar);
            return result;
        }

        public PageResult<RentalCarDto> GetAll(RentalCarQuery query)
        {
            var baseQuery = _dbContext
                .RentalCars
                // wskazuje, ze chce dolaczyc zarowno Adres jak i wartosci z tabeli Vehicles
                //tworzac takie zapytanie EntityFramework będzie wstanie dolaczyc odpowiednie tabele do wynikow zapytania
                .Include(r => r.Address)
                .Include(r => r.Vehicles)
                //zrobienie filtrowania po słowie kluczowym 
                .Where(r => query.SearchPhrase == null || (r.Name.ToLower()
                .Contains(query.SearchPhrase.ToLower()) || r.Description.ToLower()
                .Contains(query.SearchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<RentalCar, object>>>
                {
                    { nameof(RentalCar.Name), r => r.Name },
                    { nameof(RentalCar.Description), r => r.Description },
                };
                var selectedColumn = columnSelectors[query.SortBy];
                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var rentalCars = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToList();

            var totalItemCount = baseQuery.Count();

            var rentalCarsDtos = _mapper.Map<List<RentalCarDto>>(rentalCars);
            var result = new PageResult<RentalCarDto>(rentalCarsDtos, totalItemCount, query.PageSize, query.PageNumber);

            return result;
        }

        public long Create(CreateRentalCarDto dto)
        {
            //przypisanie do zmiennej, mapowania 
            var rentalCar = _mapper.Map<RentalCar>(dto);
            rentalCar.CreatedById = _userContextService.GetUserId;
            _dbContext.RentalCars.Add(rentalCar);
            _dbContext.SaveChanges();
            return rentalCar.Id;
        }

        public void Delete(long id)
        {
            _logger.LogError($"RentalCar with id: {id} DeleteById action inovoked");
            var rentalCar = _dbContext
               .RentalCars
               .Include(r => r.Address)
               .Include(r => r.Vehicles)
               .FirstOrDefault(p => p.Id == id);
            if (rentalCar is null) throw new NotFoundExceptions("Restaurant not found");

            //dzięki temu ASP.Net będzie wiedział jaki wywołać handler
            var authorizationResult = _authorizationService
               .AuthorizeAsync(_userContextService.User/*przekazujemy zalogowanego usera*/, rentalCar/*zasób*/, new ResourceOperationRequirement(ResourceOperation.Delete)).Result/*oraz informację jaki handler wykonać*/;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _dbContext.RentalCars.Remove(rentalCar);
            _dbContext.SaveChanges();
        }

        public void Update(EditRentalCarDto dto, long id)
        {
            var rentalCar = _dbContext
               .RentalCars
               .Include(r => r.Address)
               .Include(r => r.Vehicles)
               .FirstOrDefault(p => p.Id == id);

            if (rentalCar is null) throw new NotFoundExceptions("Restaurant not found");

           var authorizationResult = _authorizationService
                .AuthorizeAsync(_userContextService.User, rentalCar, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            rentalCar.Description = dto.Description;
            rentalCar.Name = dto.Name;
            rentalCar.Advance = dto.Advance;
            _dbContext.SaveChanges();
        }

    }
}