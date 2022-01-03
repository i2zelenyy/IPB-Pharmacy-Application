using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Infrastructure.Repositories
{
    public class Repository<T>: IRepository<T> where T: Entity
    {
        public PharmacyDbContext _dbContext;

        public Repository(PharmacyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Create(T e)
        {
            _dbContext.Add(e);
            return e;
        }

        public T Delete(T e)
        {         
            _dbContext.Remove(e);
            return e;
        }

        public async Task<List<T>> FindAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> FindByIdAsync(int Id)
        {
            return await _dbContext.Set<T>().FindAsync(Id);
        }

        public T Update(T e)
        {
            _dbContext.Update(e);
            return e;
        }
    }
}
