using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wieczorna_nauka_aplikacja_webowa.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //oznaczenie ? - dzięki temu data może być nullem
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string PasswordHash { get; set; }
        // klucz obcy 
        public long RoleId { get; set; }
        //dodanie Roli, która będzie danego użytkownika reprezentować 
        //służy do łączenia ze sobą tabel
        public virtual Role Role {get; set;}
    }
}