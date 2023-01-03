using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wieczorna_nauka_aplikacja_webowa
{
    public class NotFoundExceptions : Exception
    {
        public NotFoundExceptions(string message) : base (message)
        {

        }

    }
}