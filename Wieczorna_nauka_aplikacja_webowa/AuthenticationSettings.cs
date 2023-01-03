using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wieczorna_nauka_aplikacja_webowa
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; }
        public int JwtExpireDays { get; set; }
        //strona, która utowrzył token i podpisała go swoim kluczem ptywatnym - JwtIssuer.
        public string JwtIssuer { get; set; }
    }
}