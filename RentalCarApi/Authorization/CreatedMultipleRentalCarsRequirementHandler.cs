
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Wieczorna_nauka_aplikacja_webowa.Entities;

namespace Wieczorna_nauka_aplikacja_webowa.Authorization
{
    public class CreatedMultipleRentalCarsRequirementHandler : AuthorizationHandler<CreatedMultipleRentalCarsRequirement>
    {
        private readonly RentalCarDbContext _context;
        public CreatedMultipleRentalCarsRequirementHandler(RentalCarDbContext context)
        {
            _context = context;
        }
        
        //motoda w której wyci¹gniemy zalogowanego u¿tykownika i sprawdzimy czy ma dostep do danego zasobu
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRentalCarsRequirement requirement)
        {
            //wyci¹gniêcie zalogowanego u¿ytkownika
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            //wyci¹gniêcie wszystkich Wypo¿yczlani utworzonych przez Zalogowanego u¿ytkownika i zlieczenia ich za pomoc¹ Count
            var createdRentalCarsCount = _context.RentalCars.Count(r => r.CreatedById == userId);

            //sprawdzenie wypozyczalnie utworzone przez uzytkownika s¹ wiêksze ni¿ okreœlono to w wymaganiach
            if(createdRentalCarsCount >= requirement.MinimumRentalCarsCreated)
            {
                //jeœli tak przkeka¿ sukces
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
