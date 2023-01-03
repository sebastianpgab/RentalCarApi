using System.Collections.Generic;

namespace Wieczorna_nauka_aplikacja_webowa.Entities
{
    //blad podczas migracji, a dokladniej update-database !!!
    public class RentalCar
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Abroad { get; set; }
        public bool Advance { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        //klucz obcy do tabeli z adresem
        public long AddressID { get; set; } 
        /* Aby było się łatwiej posługiwać obiektem typu RentalCar,
        kiedy pobierzemy go z baz danych, będzie bezpośredni dostęp do Adresu danej Wypozyczalni, jesli
        do danych zapytan dolaczymy konkretne tabele*/
        public virtual Address Address { get; set; }  
        public virtual List<Vehicle> Vehicles { get; set; }

    }
}