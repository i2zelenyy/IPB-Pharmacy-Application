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
    public class BasketsRepository : Repository<Baskets>, IBasketsRepository
    {
        public BasketsRepository(PharmacyDbContext dbContext): base(dbContext)
        {

        }
    }
}