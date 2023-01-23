using Microsoft.AspNetCore.Authorization;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Wieczorna_nauka_aplikacja_webowa.Entities;

namespace Wieczorna_nauka_aplikacja_webowa.Authorization
{
    public class UsingMailIsGmailRequirement : AuthorizationHandler<UsingMailIsGmail>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UsingMailIsGmail requirement)
        {
            var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Email).Value;


            if (userEmail.EndsWith(requirement.MailName))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;

        }
    }
}