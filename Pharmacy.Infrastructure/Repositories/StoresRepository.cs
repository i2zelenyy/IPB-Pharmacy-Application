using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Models;
using Pharmacy.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Repositories
{
    public class StoresRepository: Repository<Stores>, IStoresRepository
    {
        public StoresRepository(PharmacyDbContext dbContext): base(dbContext)
        {

        }

        public Task<Stores> FindByNameAsync(string name)
        {
            return _dbContext.Stores.Where(e => e.Name == name).SingleOrDefaultAsync();
        }
    }
}
