using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Wieczorna_nauka_aplikacja_webowa.Entities;
using Wieczorna_nauka_aplikacja_webowa.Models;
using Wieczorna_nauka_aplikacja_webowa.Services.Services;

namespace Wieczorna_nauka_aplikacja_webowa.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal User {get; }
        public int? GetUserId { get; }
    }

    //Ta klasa będzie udostepniała informacje o użytkowniku na podstawie kontekstu HTTP 
    public class UserContextService : IUserContextService
    {
        //dostarcza informacje o zalogowanym użtykowniku
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //udostępnie iformacji o użytkowniku
        public ClaimsPrincipal User => _httpContextAccessor.HttpContext?.User;
        //id zalogowanego użtykownika
        public int? GetUserId => User is null ? null : (int?)int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }


}
