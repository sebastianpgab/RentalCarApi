using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Wieczorna_nauka_aplikacja_webowa.Entities;
using Wieczorna_nauka_aplikacja_webowa.Exceptions;
using Wieczorna_nauka_aplikacja_webowa.Models;

namespace Wieczorna_nauka_aplikacja_webowa.Controllers.Services
{
    public interface IAccountService
    {
        public void RegisterUser(RegisterUserDto dto);
        public string GenerateJwt(LoginDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly RentalCarDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        public AccountService(RentalCarDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenitactionSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenitactionSettings;
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId
            };
            //hashowanie jest jednokierunkowa tzn. po zahasowaniu danego hasła, z tego hasu nie odzyskamy juz wartosci poczatkowej
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }
        public string GenerateJwt(LoginDto dto)
        {
            var user = _dbContext.Users.Include(u => u.Role).FirstOrDefault(p => p.Email == dto.Email);
             if(user is null)
             {
                //wyjątek stworzony na własne potrzeby, utorzony został w Exceptions 
                //konstruktor wywołuje konstrukotr w Klasie Exception, a następnie dzięki Middleware
                //tworzę catcha, który przechwutuje ten wyjątek, dzięki temu sposobowi mogę korzystać z
                //kodów statusu z konkretną odpowiedzią
                throw new BadRequestException("Invalid username or password");
             }

             //sprawdza czy użytkownik podał prawidłowe hasło
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            //utworznie claimów. claimy to - konkretnych informacji,stwierdzenia np. jaka ma role o zalogowanym użytkowniku.

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")),
                new Claim("Nationality", user.Nationality)
            };
            //utworznie  prywatnego na podstawie wartosci z pliku appsettings.json

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            // utworzenie kredencjałów potrzbnych do podpisana tokenu JWT
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // określenie na ile dni ten token bedzie ważny 
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            //utworzenie tokena
            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);

        }





    }


}