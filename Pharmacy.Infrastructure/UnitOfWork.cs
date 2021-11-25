using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain;
using Pharmacy.Domain.Repositories;
using Pharmacy.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        private PharmacyDbContext _dbContext;

        public IMedicineRepository MedicineRepository => new MedicineRepository(_dbContext);
        public IStoresRepository StoresRepository => new StoresRepository(_dbContext);

        public UnitOfWork()
        {
            _dbContext = new PharmacyDbContext();
            _dbContext.Database.Migrate();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
