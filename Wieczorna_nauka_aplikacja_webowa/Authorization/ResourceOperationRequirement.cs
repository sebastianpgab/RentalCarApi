using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Wieczorna_nauka_aplikacja_webowa.Authorization
{
    //enum który reprezentuje jaka operacje chce wykonac uzytownik na konkretnym zasobie
    public enum ResourceOperation
    {
        Create,
        Read,
        Update,
        Delete
    }
    //ta klasa reprezentuje wymagania oraz typ konkretnej akcji
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperationRequirement(ResourceOperation resourceOperation)
        {
            ResourceOperation = resourceOperation;
        }
        public ResourceOperation ResourceOperation { get; }


    }
}
