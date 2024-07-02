using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Wieczorna_nauka_aplikacja_webowa.Authorization
{
    public class UsingMailIsGmail : IAuthorizationRequirement
    {

        public string MailName { get; }
        public UsingMailIsGmail(string mailName)
        {
            MailName = mailName;
        }


    }
}