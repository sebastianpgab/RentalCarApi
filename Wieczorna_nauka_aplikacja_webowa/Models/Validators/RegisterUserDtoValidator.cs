using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wieczorna_nauka_aplikacja_webowa.Entities;
using Wieczorna_nauka_aplikacja_webowa.Models;

namespace Wieczorna_nauka_aplikacja_webowa
{
    //klasa, ktora odpowiada za walidacje w modelu REgisterUserDto
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(RentalCarDbContext dbContext)
        {
            //ustawienie walidacji na email
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
            //sprawdzenie czy confirmPassword jest takie same co password
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
            //sprawdzenie, czy występuje juz taki email. 
            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUser = dbContext.Users.Any(u => u.Email == value);
                if (emailInUser)
                {
                    context.AddFailure("Email", "That email is taken");
                }
            });

        }

    }
}