using Pharmacy.Domain;
using Pharmacy.Domain.Models;
using Pharmacy.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DbContextDummy
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Stores store = new Stores("Store No.1","","","","","");

            using (IUnitOfWork uow = new UnitOfWork())
            {
                uow.StoresRepository.Create(store);
                await uow.SaveAsync();
            }

            using (IUnitOfWork uow = new UnitOfWork())
            {
                var stores = await uow.StoresRepository.FindAllAsync();
                foreach (var i in stores)
                        Console.WriteLine(i.Id + " " + store.Name);
            }
        }
    }
}
