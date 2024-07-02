
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Wieczorna_nauka_aplikacja_webowa.Entities;

namespace Wieczorna_nauka_aplikacja_webowa.Authorization
{
    public class ResourceOperationRequirementVehicleHandler : AuthorizationHandler<ResourceOperationRequirement/*klasa wymagania*/, Vehicle /*typ zasobu*/>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Vehicle vehicle)
        {
            if(requirement.ResourceOperation == ResourceOperation.Read ||
               requirement.ResourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }
            //pobranie Id u¿ytkownika 
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if(vehicle.CreatedById == int.Parse(userId))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
