using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wieczorna_nauka_aplikacja_webowa.Entities;

namespace RentalCarAPI.Services
{
    public interface IRentalCarService
    {
       // public void Create(dto);
    }
    public class RentalUserService : IRentalCarService
    {
        private readonly RentalCarDbContext dbContext;
        public RentalUserService(RentalCarDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
       /* public void Create(dto)
        {

        }*/

    }
}
