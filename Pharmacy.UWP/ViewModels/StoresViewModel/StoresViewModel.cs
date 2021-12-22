using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Domain;
using Pharmacy.Domain.Models;
using Pharmacy.Infrastructure;

namespace Pharmacy.UWP.ViewModels.StoresViewModel
{
    public class StoresViewModel
    {
        public ObservableCollection<Stores> Stores { get; set; }

        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Telephone { get; set; }
        public string OpeningHours { get; set; }

        public StoresViewModel()
        {
            // Test data!
            Stores = new ObservableCollection<Stores>()
            {
                new Stores {Name="No. 1"},
                new Stores {Name="No. 2"},
                new Stores {Name="No. 3"}
            };

            Stores = new ObservableCollection<Stores>();
        }

        public async void LoadAllAsync()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                List<Stores> list = await uow.StoresRepository.FindAllAsync();
                Stores.Clear();
                foreach (Stores i in list)
                {
                    Stores.Add(i);
                }
            }
        }

        internal async Task CreateStoreAsync()
        {

            using (IUnitOfWork uow = new UnitOfWork())
            {
                Stores store = new Stores {
                                           Name = Name, Street = Street,
                                           City = City, Country = Country,
                                           Telephone = Telephone, OpeningHours = OpeningHours
                                           };
                uow.StoresRepository.Create(store);
                await uow.SaveAsync();
            }

        }
    }
}
