using Pharmacy.Domain.Models;
using Pharmacy.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Domain.Repositories
{
    public interface IUsersRepository: IRepository<Users>
    {
        Task<Users> FindByUsernameAsync(string username);
    }
}
