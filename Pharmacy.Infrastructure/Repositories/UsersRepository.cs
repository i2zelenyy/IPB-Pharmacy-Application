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
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        public UsersRepository(PharmacyDbContext dbContext) : base(dbContext)
        {

        }

        public Task<Users> FindByUsernameAsync(string username)
        {
            return _dbContext.Users.Where(e => e.Username == username).SingleOrDefaultAsync();
        }

     
    }
}
