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
    public class MedicineRepository: Repository<Medicine>, IMedicineRepository
    {
        public MedicineRepository(PharmacyDbContext dbContext): base(dbContext)
        {

        }

        public Task<Medicine> FindByNameAsync(string name)
        {
            return _dbContext.Medicine.Where(e => e.Name == name).SingleOrDefaultAsync();
        }
    }
}
