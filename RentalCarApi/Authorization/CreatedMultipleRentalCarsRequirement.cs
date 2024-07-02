using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Wieczorna_nauka_aplikacja_webowa.Authorization
{
    //ta klasa musi implemtować IAuthorizationRequirement żeby ASP.net wiedział, że ta klasa jest wymaganiem
    public class CreatedMultipleRentalCarsRequirement : IAuthorizationRequirement
    {
        public int MinimumRentalCarsCreated { get;}
        public CreatedMultipleRentalCarsRequirement(int minimumRentalCarsCreated)
        {
            MinimumRentalCarsCreated = minimumRentalCarsCreated;
        }
    }
}
